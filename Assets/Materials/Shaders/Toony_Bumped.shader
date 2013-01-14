Shader "Custom/Toony_Bumped" {
	Properties
    {
      _MainTex ("Texture", 2D) = "white"{}
      _Ramp ("Shading Ramp", 2D) = "gray"{}
      _Bump("Bump Map", 2D) = "Bump"{}
      _Height("Height", Range (0.0, 2.0)) = 1.5
    }
    SubShader
    {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Ramp

      sampler2D _Ramp;
      float _Height;

      half4 LightingRamp (SurfaceOutput s, half3 lightDir, half atten)
      {
          half NdotL = dot (s.Normal, lightDir);
          half diff = NdotL * 0.5 + 0.5;
          half3 ramp = tex2D (_Ramp, float2(diff)).rgb;
          half4 c;
          c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
          c.a = s.Alpha;
          return c;
      }

      struct Input
      {
          float2 uv_MainTex;
          float2 uv_Bump;
      };
     
      sampler2D _MainTex;
      sampler2D _Bump;
     
      void surf (Input IN, inout SurfaceOutput o)
      {
          o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
          //o.Normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump));
         
          float3 normal = UnpackNormal ( tex2D( _Bump, IN.uv_Bump ) );
        normal.xy *= _Height;
        o.Normal = normalize ( normal );
      }
      ENDCG
    }
    Fallback "Diffuse"
}
