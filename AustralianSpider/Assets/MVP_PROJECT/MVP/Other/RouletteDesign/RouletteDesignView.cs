using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteDesignView : View
{
    [SerializeField] private List<RouletteDesign> rouletteDesignList = new List<RouletteDesign>();
    [SerializeField] private Image imageTable;
    [SerializeField] private Image imageRoulette;
    [SerializeField] private Image imageRouletteSmall;

    public void ChooseDesign(int index)
    {
        ChooseDesign(rouletteDesignList[index].SpriteTable, rouletteDesignList[index].SpriteRoulette, rouletteDesignList[index].SpriteRouletteSmall);
    }

    private void ChooseDesign(Sprite spriteTable, Sprite spriteRoulette, Sprite spriteRouletteSmall)
    {
        imageTable.sprite = spriteTable;
        imageRoulette.sprite = spriteRoulette;
        imageRouletteSmall.sprite = spriteRouletteSmall;
    }
}


[System.Serializable]
public class RouletteDesign
{
    [SerializeField] private Sprite spriteTable;
    [SerializeField] private Sprite spriteRoulette;
    [SerializeField] private Sprite spriteRouletteSmall;

    public Sprite SpriteTable => spriteTable;
    public Sprite SpriteRoulette => spriteRoulette;
    public Sprite SpriteRouletteSmall => spriteRouletteSmall;
}
