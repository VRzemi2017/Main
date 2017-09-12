using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGemLighting : MonoBehaviour {

    [SerializeField] private Light m_Light;
    [SerializeField] private Renderer m_Material;
    private float m_Time;
    private bool m_IsColorRMax = false;
    private bool m_IsColorBMax = false;
    private bool m_IsColorGMax = false;
    private void Start() {
        m_Material = GetComponent<Renderer>();
        m_Time = 0;
    }

    private void Update() { 
        m_Time += Time.deltaTime;
        Color color = m_Material.material.color;
        float dif_r = 0;
        float dif_b = 0;
        float dif_g = 0;
        if (!m_IsColorRMax) {
            dif_r = Mathf.Sin(m_Time);
        } else {
            dif_r -= Mathf.Sin(m_Time);
        }
        if (!m_IsColorBMax) {
            dif_b += Mathf.Cos(m_Time);
        } else {
            dif_b -= Mathf.Cos(m_Time);
        }
        if (!m_IsColorGMax) {
            dif_g += Mathf.Sin(m_Time / 2);
        } else {
            dif_g -= Mathf.Sin(m_Time / 2);
        }
        dif_r /= 10;
        dif_b /= 10;
        dif_g /= 10;
        color.r += dif_r;
        color.g += dif_g;
        color.b += dif_b;
        m_Material.material.SetColor("_Color", color);
        //m_Light.color = color;
        if (color.r >= 255) {
            m_IsColorRMax = true;
        }
        if (color.r <= 0) {
            m_IsColorRMax = false;
        }
        if (color.b >= 255){
            m_IsColorBMax = true;
        }
        if (color.b <= 0){
            m_IsColorBMax = false;
        }
        if (color.g >= 255){
            m_IsColorGMax = true;
        }
        if (color.g <= 0){
            m_IsColorGMax = false;
        }
    }
}
