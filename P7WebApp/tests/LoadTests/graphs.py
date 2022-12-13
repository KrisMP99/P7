import matplotlib.pyplot as plt
import pandas as pd
import numpy as np


def getvus(time_c, time_p, ramp_period, vus_p, target,):
    return vus_p + (target - vus_p)/ramp_period*(time_c-time_p)

def virtual_user_handler():
    total_time = 5741
    period = range(total_time)
    vus = []
    for i in period:
        if i < 10:
            vus.append(getvus(i,0,10,0,5))
        elif i < 70:
            vus.append(getvus(i,10,60,5,10))
        elif i < 190:
            vus.append(getvus(i,70,120,10,10))
        elif i < 220:
            vus.append(getvus(i,190,30,10,20))
        elif i < 340:
            vus.append(getvus(i,220,120,20,20))
        elif i < 370:
            vus.append(getvus(i,340,30,20,30))
        elif i < 490:
            vus.append(getvus(i,370,120,30,30))
        elif i < 520:
            vus.append(getvus(i,490,30,30,40))
        elif i < 640:
            vus.append(getvus(i,520,120,40,40))
        elif i < 670:
            vus.append(getvus(i,640,30,40,50))
        elif i < 790:
            vus.append(getvus(i,670,120,50,50))
        elif i < 820:
            vus.append(getvus(i,790,30,50,60))
        elif i < 940:
            vus.append(getvus(i,820,120,60,60))
        elif i < 970:
            vus.append(getvus(i,940,30,60,70))
        elif i < 1090:
            vus.append(getvus(i,970,120,70,70))
        elif i < 1120:
            vus.append(getvus(i,1090,30,70,80))
        elif i < 1240:
            vus.append(getvus(i,1120,120,80,80))
        elif i < 1270:
            vus.append(getvus(i,1240,30,80,90))
        elif i < 1390:
            vus.append(getvus(i,1270,120,90,90))
        elif i < 1420:
            vus.append(getvus(i,1390,30,90,100))
        elif i < 1540:
            vus.append(getvus(i,1420,120,100,100))
        elif i < 1570:
            vus.append(getvus(i,1540,30,100,110))
        elif i < 1690:
            vus.append(getvus(i,1570,120,110,110))
        elif i < 1720:
            vus.append(getvus(i,1690,30,110,120))
        elif i < 1840:
            vus.append(getvus(i,1720,120,120,120))
        elif i < 1870:
            vus.append(getvus(i,1840,30,120,130))
        elif i < 1990:
            vus.append(getvus(i,1870,120,130,130))
        elif i < 2020:
            vus.append(getvus(i,1990,30,130,140))
        elif i < 2140:
            vus.append(getvus(i,2020,120,140,140))
        elif i < 2170:
            vus.append(getvus(i,2140,30,140,150))
        elif i < 2290:
            vus.append(getvus(i,2170,120,150,150))
        elif i < 2320:
            vus.append(getvus(i,2290,30,150,160))
        elif i < 2440:
            vus.append(getvus(i,2320,120,160,160))
        elif i < 2470:
            vus.append(getvus(i,2440,30,160,170))
        elif i < 2590:
            vus.append(getvus(i,2470,120,170,170))
        elif i < 2620:
            vus.append(getvus(i,2590,30,170,180))
        elif i < 2740:
            vus.append(getvus(i,2620,120,180,180))
        elif i < 2770:
            vus.append(getvus(i,2740,30,180,190))
        elif i < 2890:
            vus.append(getvus(i,2770,120,190,190))
        elif i < 2920:
            vus.append(getvus(i,2890,30,190,200))
        elif i < 3040:
            vus.append(getvus(i,2920,120,200,200))
        elif i < 3070:
            vus.append(getvus(i,3040,30,200,210))
        elif i < 3190:
            vus.append(getvus(i,3070,120,210,210))
        elif i < 3220:
            vus.append(getvus(i,3190,30,210,220))
        elif i < 3340:
            vus.append(getvus(i,3220,120,220,220))
        elif i < 3370:
            vus.append(getvus(i,3340,30,220,230))
        elif i < 3490:
            vus.append(getvus(i,3370,120,230,230))
        elif i < 3520:
            vus.append(getvus(i,3490,30,230,240))
        elif i < 3640:
            vus.append(getvus(i,3520,120,240,240))
        elif i < 3670:
            vus.append(getvus(i,3640,30,240,250))
        elif i < 3790:
            vus.append(getvus(i,3640,120,250,250))
        elif i < 3820:
            vus.append(getvus(i,3790,30,250,260))
        elif i < 3940:
            vus.append(getvus(i,3820,120,260,260))
        elif i < 3970:
            vus.append(getvus(i,3940,30,260,270))
        elif i < 4090:
            vus.append(getvus(i,3970,120,270,270))
        elif i < 4120:
            vus.append(getvus(i,4090,30,270,280))
        elif i < 4240:
            vus.append(getvus(i,4120,120,280,280))
        elif i < 4270:
            vus.append(getvus(i,4240,30,280,290))
        elif i < 4390:
            vus.append(getvus(i,4270,120,290,290))
        elif i < 4420:
            vus.append(getvus(i,4390,30,290,300))
        elif i < 4540:
            vus.append(getvus(i,4420,120,300,300))
        elif i < 4570:
            vus.append(getvus(i,4540,30,300,350))
        elif i < 4690:
            vus.append(getvus(i,4570,120,350,350))
        elif i < 4720:
            vus.append(getvus(i,4690,30,350,400))
        elif i < 4840:
            vus.append(getvus(i,4720,120,400,400))
        elif i < 4870:
            vus.append(getvus(i,4840,30,400,450))
        elif i < 4990:
            vus.append(getvus(i,4870,120,450,450))
        elif i < 5020:
            vus.append(getvus(i,4990,30,450,500))
        elif i < 5140:
            vus.append(getvus(i,5020,120,500,500))
        elif i < 5260:
            vus.append(getvus(i,5140,120,500,400))
        elif i < 5380:
            vus.append(getvus(i,5260,120,400,300))
        elif i < 5500:
            vus.append(getvus(i,5380,120,300,200))
        elif i < 5620:
            vus.append(getvus(i,5500,120,200,100))
        elif i < 5740:
            vus.append(getvus(i,5620,120,100,50))
        else:
            vus.append(getvus(i,5740,120,50,50))
    return vus
            

def virtual_user_handler_big_cache():
    total_time = 2901
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
    return vus

def ProduceGraph(csv_file, specificer):
    print("Process " + specificer + " started")

    columns = ["metric_name", "timestamp", "metric_value", "group"]
    df = pd.read_csv(csv_file, usecols=columns, dtype={'metric_name': 'object', 'timestamp': 'int64', 'metric_value': 'float64', 'group' : 'object'})
    
    df = df.loc[df["metric_name"] == "http_req_duration"] 
    df = df.loc[df["group"] != "::setup"]

    print("Length of df: " + len(df))
    quit()

    df["timestamp"] = pd.to_datetime(df["timestamp"], unit='s')
    df["elapsed_time"] = df["timestamp"] - df["timestamp"].iat[0]
    
    df = df.groupby(pd.Grouper(key="timestamp", freq="1s"))['metric_value'].agg(['sum', 'count'])
    df["virtual_users"] = virtual_user_handler_big_cache()
    df = df.rename(columns={'sum':'sum_response_time', 'count':'number_of_responses'})
    df["timestamp"] = df.index

    df2 = df.groupby(pd.Grouper(key="timestamp", freq="30s")).agg({'sum_response_time':['sum', 'count'], 'number_of_responses':['sum', 'count'], 'virtual_users':['sum', 'count']})
    df2['sum_response_time_avg'] = df2['sum_response_time']['sum'] / df2['sum_response_time']['count']
    df2['number_response_avg'] = df2['number_of_responses']['sum'] / df2['number_of_responses']['count']
    df2['virtual_users_avg'] = df2['virtual_users']['sum'] / df2['virtual_users']['count']
    
    df2['avg_response_time'] = df2['sum_response_time_avg']/df2['number_response_avg']
    df2 = df2.drop(columns=['sum_response_time', 'number_of_responses'])


# CASE FOR AVERAGE REQUEST RESPONSE TIME PR. 30 SECONDS 
    df2 = df2.reset_index()
    df2["time"] = df2.index * 30
    fig, ax = plt.subplots()
    
    ax.set_title('Average request response time pr. 30 seconds')
    ax.set_xlabel("Time elapsed in 30 seconds intervals")
    ax.set_ylabel("Average request response time (ms)")
    ax.plot(df2["time"], df2["avg_response_time"], 'bo')
    

    ax2 = ax.twinx()
    ax2.plot(df2["time"], df2["virtual_users_avg"], color="red")
    ax2.set_ylabel("Virtual Users")

# CASE FOR AVEAGE NUMBER OF REQUEST PR. 30 SECONDS
    # df2 = df2.reset_index()
    # df2["time"] = df2.index * 30
    # fig, ax = plt.subplots()

    # ax.set_title('Average number of requests pr. 30 seconds')
    # ax.plot(df2["time"], df2["number_response_avg"], 'bo')
    # ax.set_xlabel("Time elapsed in 30 seconds intervals")
    # ax.set_ylabel("Average number of requests")

    # ax2 = ax.twinx()
    # ax2.plot(df2["time"], df2["virtual_users_avg"], color="red")
    # ax2.set_ylabel("Virtual Users")

    print("Process " + specificer + " completed.")

def main():
    #ProduceGraph("Testdata_no_cache_500_vus.csv", "1")
    #ProduceGraph("Testdata_cache_500_vus.csv", "2")
    #ProduceGraph("Testdata_cache_10000_vus.csv", "3")

    #plt.show()

    import tikzplotlib
    tikzplotlib.save("testdata.tex")


main()