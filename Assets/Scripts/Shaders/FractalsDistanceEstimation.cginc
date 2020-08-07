//(Infinite) Plane
//n.xyz: normal of the plane (normalized)
//n.w: offset
float sdPlane(float3 p, float4 n){
    //n must be normalized
    return dot(p, n.xyz)+ n.w;
}

// FRACTALS //
//recursive Tetrahedron
float sdTetrahedron(float3 pos, int iterations, float scale){
    //scale=scale *(abs(cos(_Time.y*.1)));
    float3 a1=float3(  0,  0.5, 0) * scale;
    float3 a2=float3(  0, -1,  1 ) * scale;
    float3 a3=float3(  1, -1, -0.5)* scale;
    float3 a4=float3( -1, -1, -0.5)* scale;
    
    float3 c;
    float dist, d;
    int i;
    for(i=0; i <iterations; i++){
        c=a1;
        dist=length(pos-a1);
        
        d=length(pos-a2);
        if(d<dist){
            c=a2;
            dist=d;
        }
        
        d=length(pos-a3);
        if(d<dist){
            c=a3;
            dist=d;
        }
        
        d=length(pos-a4);
        if(d<dist){
            c=a4;
            dist=d;
        }
        pos=2.0*pos - c;      
    }
    return length(pos)*pow(2.0, float(-i));
}

//paxis
float3 fsign(float3 p){
    return float3(
            p.x<0.0? -1.0 : 1.0,
            p.y<0.0? -1.0 : 1.0,
            p.z<0.0? -1.0 : 1.0
    );
}
float3 paxis(float3 p){
    float3 a=abs(p);
    return fsign(p) * max(fsign(a - max(a.yzx, a.zxy)),0.0);
}

float3 paxis2(float3 p){
    float3 a=abs(p);
    return fsign(p) * max(fsign(a - min(a.yzx, a.zxy)),0.0);
}

float sdPaxis(float3 pos, int iter1, int iter2, float mult, int iter1Swap, int iter2Swap){
    float b=1.0;
    
    // change both 'fors' between paxis and paxis2 to create different fractals
    // CAREFUL with the number of iterations in both
    for (int i=0; i<iter1;i++){
        if(iter1Swap==0)
            pos-=paxis(pos) * b* abs(sin(_Time.y*0.1));
        else
            pos-=paxis2(pos) * b* abs(sin(_Time.y*0.1));
        b*=0.5;
    }
    float d=length(pos)-mult;   //0.1<=mult<=0.3
    
    for (i=0; i<iter2;i++){
        if(iter2Swap==0)
            pos-=paxis2(pos) * b;
        else
            pos-=paxis(pos) * b;
        b*=0.5;
    }
    
    pos=abs(pos);
    float d2=max(pos.x, max(pos.y, pos.z))-b;
    return max(d2,-d);
}


float sdMandelbulb3D(float3 pos, int mandelIterations){
    float3 w=pos;
    float m=dot(w, w);
    
    float4 trap=float4(abs(w), m);
        
    float dz=2;
    
    for (int i=0;i<mandelIterations;i++){
        float m2=m*m;
        float m4=m2*m2;
        dz*=8*sqrt(m4*m2*m)+1;
        
        float x = w.x; float x2 = x*x; float x4 = x2*x2;
        float y = w.y; float y2 = y*y; float y4 = y2*y2;
        float z = w.z; float z2 = z*z; float z4 = z2*z2;
        
        float k3=x2+z2;
        float k2 = 1.0/sqrt( k3*k3*k3*k3*k3*k3*k3 );
        float k1 = x4 + y4 + z4 - 6.0*y2*z2 - 6.0*x2*y2 + 2.0*z2*x2;
        float k4 = x2 - y2 + z2;
        
        w.x = pos.x +  64.0*x*y*z*(x2-z2)*k4*(x4-6.0*x2*z2+z4)*k1*k2;
        w.y = pos.y + -16.0*y2*k3*k4*k4 + k1*k1;
        w.z = pos.z +  -8.0*y*k4*(x4*x4 - 28.0*x4*x2*z2 + 70.0*x4*z4 - 28.0*x2*z2*z4 + z4*z4)*k1*k2;

        trap=min(trap, float4(abs(w), m));
        
        m=dot(w,w);
        if(m>4) break;
        
    }
    
    return .5*log(m)*sqrt(m)/dz;
}