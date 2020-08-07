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



//Mandelbulb3D
float sdMandelbulb3D(float3 p, int mandelIterations){
   float3 z = p;
    
    {
        const float exBR = 1.5;
        float r = length(p)-exBR;
        if(r>1.0){return r;}
    }
    
    float dr =1., r=0., pw = 8., fr=.0, theta, phi, zr;
    for(int i=0;i<mandelIterations;i++)
    {
        
        r=length(z);
        if(r>2.)
        {
            fr = min(0.5*log(r)*r/dr, length(p)-.72);
            break;
        }
        theta=acos(z.z/r)+_Time.y/10.;
        phi = atan2(z.y,z.x);
        dr = pow(r,7.)*7.*dr+1.;
        
        zr = pow(r,pw);
        theta = theta*pw;
        phi = phi*pw;
        
        z=zr*float3(sin(theta)*cos(phi),
                  sin(phi)*sin(theta),
                  cos(theta))+p;
    }
    
    return fr;
}