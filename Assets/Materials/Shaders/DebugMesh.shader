Shader "Custom/DebugMesh" {
 SubShader {
      Pass {
  
   CGPROGRAM
   #pragma vertex vert
   #pragma fragment frag
   #include "UnityCG.cginc"
   
   struct v2f {
       float4 pos : SV_POSITION;
       float3 color : COLOR0;
   };
   
   v2f vert (appdata_base v)
   {
       v2f o;
       o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
       o.color = float4( v.texcoord.xy, 0, 0 );
       return o;
   }
   
   half4 frag (v2f i) : COLOR
   {
       return half4 (i.color, 1);
   }
   ENDCG
 
     }
 }
 Fallback "VertexLit"
}
