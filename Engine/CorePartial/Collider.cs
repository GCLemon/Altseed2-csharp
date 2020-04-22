﻿using System;
using System.Runtime.Serialization;

namespace Altseed
{
    public partial class CircleCollider
    {
        partial void Deserialize_GetPtr(ref IntPtr ptr, SerializationInfo info) => ptr = cbg_CircleCollider_Constructor_0();
    }
    public partial class RectangleCollider
    {
        partial void Deserialize_GetPtr(ref IntPtr ptr, SerializationInfo info) => ptr = cbg_RectangleCollider_Constructor_0();
    }
    public partial class PolygonCollider
    {
        /// <summary>
        /// 頂点情報の配列を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentNullException">設定しようとした値がnull</exception>
        public Vector2F[] VertexArray
        {
            get => Vertexes.ToArray();
            set
            {
                var array = Vector2FArray.Create(Vertexes.Count);
                array.FromArray(value);
                Vertexes = array;
            }
        }

        partial void Deserialize_GetPtr(ref IntPtr ptr, SerializationInfo info) => ptr = cbg_PolygonCollider_Constructor_0();
    }
}
