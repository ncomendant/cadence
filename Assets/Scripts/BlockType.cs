using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockType {
    public static readonly BlockType W = new BlockType(KeyCode.W, "W", 4f);
    public static readonly BlockType A = new BlockType(KeyCode.A, "A", 3f);
    public static readonly BlockType S = new BlockType(KeyCode.S, "S", 2f);
    public static readonly BlockType D = new BlockType(KeyCode.D, "D", 1f);

    public static readonly BlockType UP = new BlockType(KeyCode.UpArrow, "\u25b2", -1f);
    public static readonly BlockType LEFT = new BlockType(KeyCode.LeftArrow, "\u25c0", -2f);
    public static readonly BlockType DOWN = new BlockType(KeyCode.DownArrow, "\u25bc", -3f);
    public static readonly BlockType RIGHT = new BlockType(KeyCode.RightArrow, "\u25b6", -4f);

    public static readonly BlockType[] VALUES = new BlockType[]{BlockType.W, BlockType.A, BlockType.S, BlockType.D, BlockType.UP, BlockType.LEFT, BlockType.DOWN, BlockType.RIGHT};

    public KeyCode keyCode;
    public string text;
    public float position;

    public BlockType(KeyCode keyCode, string text, float position)
    {
        this.keyCode = keyCode;
        this.text = text;
        this.position = position;
    }
}
