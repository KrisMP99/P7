import numpy as np


def getvus(time_c, time_p, ramp_period, vus_p, target,):
    return vus_p + (target - vus_p)/ramp_period*(time_c-time_p)


def main():
    total_time = 2850
    period = range(total_time)
    vus = []
    for i in period:
        if i < 60:
            vus.append(getvus(i,0,60,0,500))
        elif i < 90:
            vus.append(getvus(i,60,30,500,500))
        elif i < 150:
            vus.append(getvus(i,90,60,500,750))
        elif i < 270:
            vus.append(getvus(i,150,120,750,750))
        elif i < 330:
            vus.append(getvus(i,270,60,750,1000))
        elif i < 450:
            vus.append(getvus(i,330,120,1000,1000))
        elif i < 570:
            vus.append(getvus(i, 450, 120, 1000, 1500))
        elif i < 630:
            vus.append(getvus(i,570,60,1500,1500))
        elif i < 750:
            vus.append(getvus(i,630,120, 1500,2000))
        elif i < 810:
            vus.append(getvus(i,750,60,2000,2000))
        elif i < 930:
            vus.append(getvus(i,810,120,2000,2250))
        elif i < 990:
            vus.append(getvus(i,930,60,2250,2250))
        elif i < 1110:
            vus.append(getvus(i,990,120,2250,2500))
        elif i < 1180:
            vus.append(getvus(i,1110,60,2500,2500))
        elif i < 1300:
            vus.append(getvus(i,1180,120,2500,2750))
        elif i < 1360:
            vus.append(getvus(i,1300,60,2750,2750))
        elif i < 1480:
            vus.append(getvus(i,1360,120,2750,3000))
        elif i < 1640:
            vus.append(getvus(i,1480,60,3000,3000))
        elif i < 1760:
            vus.append(getvus(i,1640,120,3000,3250))
        elif i < 1820:
            vus.append(getvus(i,1760,60,3250,3250))
        elif i < 1940:
            vus.append(getvus(i,1820,120,3250,3500))
        elif i < 2000:
            vus.append(getvus(i,1940, 60,3500,3500))
        elif i < 2120:
            vus.append(getvus(i,2000,120,3500,4000))
        elif i < 2180:
            vus.append(getvus(i,2120,60,4000,4000))
        elif i < 2300:
            vus.append(getvus(i,2180,120,4000,5000))
        elif i < 2600:
            vus.append(getvus(i,2300,300,5000,5000))
        elif i < 2720:
            vus.append(getvus(i,2600,120,5000,6000))
        elif i < 2780:
            vus.append(getvus(i,2720,60,6000,6000))
        elif i < 2900:
            vus.append(getvus(i,2780,120,600,10000))
        else:
            vus.append(getvus(i,2900,60,10000,10000)) 

        



if __name__ == "__main__":
    main()
            


        
            

    
