//Mod Position Axis
float pMod(inout float p, float size){
    float halfsize= size * 0.5;
    float c=floor((p+halfsize)/size);
    p= fmod(p+halfsize, size)-halfsize; 
    p= fmod(-p+halfsize, size)-halfsize;
    
    return c; 
}

// BOOLEAN OPERATORS //

//Union
float opU(float d1, float d2){
    return min(d1,d2);
}

//Subtraction
float opS(float d1, float d2){
    return max(-d1, d2);
}

//Intersection
float opI(float d1, float d2){
    return max(d1, d2);
}

// SMOOTH BOOLEAN OPERATORS //

//Smooth Union
float4 opUS(float4 d1, float4 d2, float k){
    float h=clamp(0.5 + 0.5*(d2.w-d1.w)/k, 0.0, 1.0);
    float3 color= lerp(d2.rgb, d1.rgb, h);
    float dist= lerp(d2.w, d1.w, h) - k*h*(1.0-h);
    return float4 (color, dist);
}

//Smooth Subtraction
float opSS(float d1, float d2, float k){
    float h=clamp(0.5 - 0.5*(d2+d1)/k, 0.0, 1.0);
    return lerp(d2, -d1, h) + k*h*(1.0-h);
}

//Smooth Intersection
float opIS(float d1, float d2, float k){
    float h=clamp(0.5 - 0.5*(d2-d1)/k, 0.0, 1.0);
    return lerp(d2, d1, h) + k*h*(1.0-h);
}
