//////////////////////////////////////////////////////////////
/// Shadero Sprite: Sprite Shader Editor - by VETASOFT 2018 //
/// Shader generate with Shadero 1.9.6                      //
/// http://u3d.as/V7t #AssetStore                           //
/// http://www.shadero.com #Docs                            //
//////////////////////////////////////////////////////////////

Shader "Shadero Customs/Aura Light"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
_Hologram_Value_1("_Hologram_Value_1", Range(0, 1)) = 0.885
_Hologram_Speed_1("_Hologram_Speed_1", Range(0, 4)) = 2.442
_TintRGBA_Color_1("_TintRGBA_Color_1", COLOR) = (0.6886792,0.2371395,0.2371395,1)
_InnerGlowHQ_Intensity_1("_InnerGlowHQ_Intensity_1", Range(1, 16)) = 0
_InnerGlowHQ_Size_1("_InnerGlowHQ_Size_1", Range(1, 16)) = 3
_InnerGlowHQ_Color_1("_InnerGlowHQ_Color_1", COLOR) = (1,0.03997353,0,1)
_AlphaAsAura_Fade_1("_AlphaAsAura_Fade_1", Range(0, 1)) = 1
_SpriteFade("SpriteFade", Range(0, 1)) = 1.0

// required for UI.Mask
[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
[HideInInspector]_Stencil("Stencil ID", Float) = 0
[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255
[HideInInspector]_ColorMask("Color Mask", Float) = 15

}

SubShader
{

Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off 

// required for UI.Mask
Stencil
{
Ref [_Stencil]
Comp [_StencilComp]
Pass [_StencilOp]
ReadMask [_StencilReadMask]
WriteMask [_StencilWriteMask]
}

Pass
{

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

struct appdata_t{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
float2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
float4 color    : COLOR;
};

sampler2D _MainTex;
float _SpriteFade;
float _Hologram_Value_1;
float _Hologram_Speed_1;
float4 _TintRGBA_Color_1;
float _InnerGlowHQ_Intensity_1;
float _InnerGlowHQ_Size_1;
float4 _InnerGlowHQ_Color_1;
float _AlphaAsAura_Fade_1;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}


inline float Holo1mod(float x,float modu)
{
return x - floor(x * (1.0 / modu)) * modu;
}

inline float Holo1noise(sampler2D source,float2 p)
{
float _TimeX = _Time.y;
float sample = tex2D(source,float2(.2,0.2*cos(_TimeX))*_TimeX*8. + p*1.).x;
sample *= sample;
return sample;
}

inline float Holo1onOff(float a, float b, float c)
{
float _TimeX = _Time.y;
return step(c, sin(_TimeX + a*cos(_TimeX*b)));
}

float4 Hologram(float2 uv, sampler2D source, float value, float speed)
{
float alpha = tex2D(source, uv).a;
float _TimeX = _Time.y * speed;
float2 look = uv;
float window = 1. / (1. + 20.*(look.y - Holo1mod(_TimeX / 4., 1.))*(look.y - Holo1mod(_TimeX / 4., 1.)));
look.x = look.x + sin(look.y*30. + _TimeX) / (50.*value)*Holo1onOff(4., 4., .3)*(1. + cos(_TimeX*80.))*window;
float vShift = .4*Holo1onOff(2., 3., .9)*(sin(_TimeX)*sin(_TimeX*20.) + (0.5 + 0.1*sin(_TimeX*20.)*cos(_TimeX)));
look.y = Holo1mod(look.y + vShift, 1.);
float4 video = float4(0, 0, 0, 0);
float4 videox = tex2D(source, look);
video.r = tex2D(source, look - float2(.05, 0.)*Holo1onOff(2., 1.5, .9)).r;
video.g = videox.g;
video.b = tex2D(source, look + float2(.05, 0.)*Holo1onOff(2., 1.5, .9)).b;
video.a = videox.a;
video = video;
float vigAmt = 3. + .3*sin(_TimeX + 5.*cos(_TimeX*5.));
float vignette = (1. - vigAmt*(uv.y - .5)*(uv.y - .5))*(1. - vigAmt*(uv.x - .5)*(uv.x - .5));
float noi = Holo1noise(source,uv*float2(0.5, 1.) + float2(6., 3.))*value * 3;
float y = Holo1mod(uv.y*4. + _TimeX / 2. + sin(_TimeX + sin(_TimeX*0.63)), 1.);
float start = .5;
float end = .6;
float inside = step(start, y) - step(end, y);
float fact = (y - start) / (end - start)*inside;
float f1 = (1. - fact) * inside;
video += f1*noi;
video += Holo1noise(source,uv*2.) / 2.;
video.r *= vignette;
video *= (12. + Holo1mod(uv.y*30. + _TimeX, 1.)) / 13.;
video.a = video.a + (frac(sin(dot(uv.xy*_TimeX, float2(12.9898, 78.233))) * 43758.5453))*.5;
video.a = (video.a*.3)*alpha*vignette * 2;
video.a *=1.2;
video.a *= 1.2;
video = lerp(tex2D(source, uv), video, value);
return video;
}
float4 TintRGBA(float4 txt, float4 color)
{
float3 tint = dot(txt.rgb, float3(.222, .707, .071));
tint.rgb *= color.rgb;
txt.rgb = lerp(txt.rgb,tint.rgb,color.a);
return txt;
}
float2 PixelXYUV(float2 uv, float x, float y)
{
float2 pos = float2(x, y);
uv = floor(uv * pos+0.5) / pos;
return uv;
}
float InnerGlowAlpha(sampler2D source, float2 uv)
{
return (1 - tex2D(source, uv).a);
}
float4 InnerGlow(float2 uv, sampler2D source, float Intensity, float size, float4 color)
{
float step1 = 0.00390625f * size*2;
float step2 = step1 * 2;
float4 result = float4 (0, 0, 0, 0);
float2 texCoord = float2(0, 0);
texCoord = uv + float2(-step2, -step2); result += InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(-step1, -step2); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(0, -step2); result += 6.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(step1, -step2); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(step2, -step2); result += InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(-step2, -step1); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(-step1, -step1); result += 16.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(0, -step1); result += 24.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(step1, -step1); result += 16.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(step2, -step1); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(-step2, 0); result += 6.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(-step1, 0); result += 24.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv; result += 36.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(step1, 0); result += 24.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(step2, 0); result += 6.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(-step2, step1); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(-step1, step1); result += 16.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(0, step1); result += 24.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(step1, step1); result += 16.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(step2, step1); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(-step2, step2); result += InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(-step1, step2); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(0, step2); result += 6.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(step1, step2); result += 4.0 * InnerGlowAlpha(source, texCoord);
texCoord = uv + float2(step2, step2); result += InnerGlowAlpha(source, texCoord);
result = result*0.00390625;
result = lerp(tex2D(source,uv),color*Intensity,result*color.a);
result.a = tex2D(source, uv).a;
return saturate(result);
}
float4 AlphaAsAura(float4 origin, float4 overlay, float blend)
{
float4 o = origin;
o = o.a;
if (o.a > 0.99) o = 0;
float4 aura = lerp(origin, origin + overlay, blend);
o = lerp(origin, aura, o);
return o;
}
float4 frag (v2f i) : COLOR
{
float4 _Hologram_1 = Hologram(i.texcoord,_MainTex,_Hologram_Value_1,_Hologram_Speed_1);
float4 TintRGBA_1 = TintRGBA(_Hologram_1,_TintRGBA_Color_1);
float4 _InnerGlowHQ_1 = InnerGlow(i.texcoord,_MainTex,_InnerGlowHQ_Intensity_1,_InnerGlowHQ_Size_1,_InnerGlowHQ_Color_1);
float4 AlphaAsAura_1 = AlphaAsAura(TintRGBA_1, _InnerGlowHQ_1, _AlphaAsAura_Fade_1); 
float4 FinalResult = AlphaAsAura_1;
FinalResult.rgb *= i.color.rgb;
FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
return FinalResult;
}

ENDCG
}
}
Fallback "Sprites/Default"
}
