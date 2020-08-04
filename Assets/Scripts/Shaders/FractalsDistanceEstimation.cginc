float sdTetrahedron(float3 pos, int iterations, float scale){
    float3 a1=float3(1, 1, 1);
    float3 a2=float3(-1, -1, 1);
    float3 a3=float3(1, -1, -1);
    float3 a4=float3(-1, 1, -1);
    
    float3 c;
    float dist, d;
    for(int i=0; i <iterations; i++){
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
        pos=scale*pos - c* (scale-1.0);      
    }
    
    return length(pos)*pow(scale, float(-n));
}