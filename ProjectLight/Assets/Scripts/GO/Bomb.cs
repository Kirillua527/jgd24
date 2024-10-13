using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]  private float     m_explosion_range = 5f;
    [SerializeField]  private float     m_explosion_width = 0.5f;
    [SerializeField]  private float     m_ray_visible_duration = 1.0f;
    [SerializeField]  private int       m_ray_count_per_direction = 3;
    [SerializeField]  private LayerMask m_ray_layer_mask;

    private float m_ray_spacing = 0.1f;

    //private LineRenderer[] m_line_renderers;

    private void Start()
    {
        m_ray_spacing = m_explosion_width / m_ray_count_per_direction;
    }

    public void Explode()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 direction in directions)
        {
            ShootParallelRays(direction);
        }
    }

    private void ShootParallelRays(Vector2 base_direction)
    {
        Vector2 perpendicularOffset = Vector2.Perpendicular(base_direction) * m_ray_spacing;
        Vector2 origin = transform.position;

        for (int i = 0; i < m_ray_count_per_direction; i++)
        {
            float offsetMultiplier = (i - (m_ray_count_per_direction - 1) / 2f);
            Vector2 ray_origin = origin + offsetMultiplier * perpendicularOffset;

            RaycastHit2D hit = Physics2D.Raycast(ray_origin, base_direction, m_explosion_range, m_ray_layer_mask);

            // debug draw
            StartCoroutine(VisualizeRay(ray_origin, base_direction * m_explosion_range, m_ray_visible_duration));

            if (hit)
            {
                Debug.Log("Ray hit: " + hit.collider.name);
            }
        }
    }

    private IEnumerator VisualizeRay(Vector3 start, Vector3 dir_length, float duration)
    {
        // visualize ray in editor
        Debug.DrawRay(start, dir_length, Color.red, duration);

        // visualize ray in game
        GameObject line_obj = new GameObject("Ray");
        line_obj.transform.SetParent(transform);

        LineRenderer line_renderer = line_obj.AddComponent<LineRenderer>();
        line_renderer.positionCount = 2;
        line_renderer.SetPosition(0, start);
        line_renderer.SetPosition(1, start + dir_length);
        line_renderer.startWidth = 0.1f;
        line_renderer.endWidth = 0.1f;
        line_renderer.material = new Material(Shader.Find("Sprites/Default"));
        line_renderer.startColor = Color.red;
        line_renderer.endColor = Color.red;

        yield return YieldHelper.WaitForSeconds(duration);
        Destroy(line_obj);
    }
}
