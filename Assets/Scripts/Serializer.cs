using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    struct StrippedBlock
    {
        public float X, Y, ScaleX, ScaleY;
        public string Tag;

    }
    static class Serializer
    {
        public static StrippedBlock StripBlock(GameObject obj) {
            StrippedBlock block;
            block.X = obj.transform.position.x;
            block.Y = obj.transform.position.y;
            block.ScaleX = obj.transform.localScale.x;
            block.ScaleY = obj.transform.localScale.y;
            block.Tag = obj.tag;
            return block;
        }
    }
}
