using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Human : Player
{
    [SerializeField]
    protected Sprite m_FullHeart;
    [SerializeField]
    protected Sprite m_HalfHeart;
    [SerializeField]
    protected Sprite m_EmptyHeart;

    [SerializeField]
    protected List<Image> m_HeartImage;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        m_HitPoints = 4;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        for(int i = 0;i< m_HeartImage.Count;++i)
        {
            if (m_HitPoints >= i + 1)
                m_HeartImage[i].sprite = m_FullHeart;
            else if (m_HitPoints > i)
                m_HeartImage[i].sprite = m_HalfHeart;
            else
                m_HeartImage[i].sprite = m_EmptyHeart;
        }

        if (m_HitPoints <= 0)
            TheGame.GameOver();
    }
    
    protected override void OnValidate()
    {
        base.OnValidate();

        if (m_HitPoints > m_HeartImage.Count)
            m_HitPoints = m_HeartImage.Count;
    }
}
