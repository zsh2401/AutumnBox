/* =============================================================================*\
*
* Filename: RunningManager.cs
* Description: 
*
* Version: 1.0
* Created: 9/27/2017 02:53:58(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
// 御坂妹妹~ 不过在Visual Studio 2017下似乎需要调字体间距
// 御坂妹保佑不出BUG!
//                                                     /*[*/#include<stdio.h>//
//                         #include<stdlib.h>//]++++[->++[->+>++++<<]<][(c)2013]
//                        #ifndef                                           e//[o
//                       #include<string.h>//]![misaka.c,size=3808,crc=d0ec3b36][
//                      #define e                                           0x1//
//                    typedef struct{int d,b,o,P;char*q,*p;}f;int p,q,d,b,_=0//|
//                  #include __FILE__//]>>>[->+>++<<]<[-<<+>>>++<]>>+MISAKA*IMOUTO
//                #undef e//[->[-<<+<+<+>>>>]<<<<<++[->>+>>>+<<<<<]>+>+++>+++[>]]b
//             #define e(c)/**/if((_!=__LINE__?(_=__LINE__):0)){c;}//[20002,+[-.+]
//            ,O,i=0,Q=sizeof(f);static f*P;static FILE*t;static const char*o[]={//
//          "\n\40\"8oCan\40not\40open %s\n\0aaFbfeccdeaEbgecbbcda6bcedd#e(bbed$bbd",
//        "a6bgcdbbccd#ead$c%bcdea7bccde*b$eebbdda9bsdbeccdbbecdcbbcceed#eaa&bae$cbe",
//       "e&cbdd$eldbdeedbbdede)bdcdea&bbde1bedbbcc&b#ccdee&bdcdea'bbcd)e'bad(bae&bccd",
//      "e&bbda1bdcdee$bbce#b$c&bdedcd%ecdca4bhcdeebbcd#e$b#ecdcc$bccda7bbcc#e#d%c*bbda",
//     ">bad/bbda"};static int S(){return(o[p][q]);}static/**/int/**/Z=0  ;void/**/z(int//
//    l){if(/**/Z-l){Z=l;q++;if(p<b*5&&!S()){p+=b;q=0;}}}int main(int I,    /**/char**l){//
//   d=sizeof(f*);if(1<(O=_)){b=((sizeof(o)/sizeof(char*))-1)/4;q=22; p=     0;while(p<b*5){
//  /*<*/if(Z-1){d=S()>96;i=S()-(d?96:32) ;q++;if(p<b*5&&!S()){p+=b;  q=      0;}Z=1;}/*[[*/
//  while(i){_=o[0][S()-97];I=_-10?b:1;   for( ;I--;)putchar(_ );if   (!      --i||d)z(~i );}
// if(p==b*5&&O){p-=b;O--;}}return 0U;   }if(! (P=( f*)calloc /*]*/  (Q        ,I)))return 1;
// {;}for(_=p=1;p<I;p++){e(q=1);while    (q<   p&&  strcmp(  l[p     ]         ,l[(q)]))++  q;
// t=stdin;if(q<p){(void)memcpy/* "      */    (&P  [p],&P   [q     ]          ,Q);continue ;}
//if(strcmp(l[p],"-")){t=fopen(l         [     p]   ,"rb"   )                  ;if(!t ){{;}  ;
//printf(05+*o,l[p ]);return+1;                      {;}                       }}_=b= 1<<16   ;
//*&O=5;do{if(!(P[p].q=realloc   (P[p].q,(P[p].P     +=       b)+1))){return   01;}O   &=72   /
//6/*][*/;P[p].o+=d=fread(P[p]      .q       +P[     p           ].       o,  1,b,t)   ;}//
// while(d==b)      ;P [p].q[       P[       p]                  .o       ]=  012;d    =0;
// e(fclose(t        )  );P         [p]      .p                  =P[      p]  .q;if    (O)
//{for(;d<P[            p]          .o     ;d=                   q+     1)    {q=     d;
//  while(q<P[                        p].o&&P[                    p].q[q]-     10     ){
//  q++;}b=q-d;                         _=P                         [p].        d     ;
//  if(b>_){/*]b                                                                */
//   P[p].d=b;}{;                                                                }
//   #undef/*pqdz'.*/  e//                                                      ;
//   #define/*s8qdb]*/e/**/0                                                   //
//   //<<.<<.----.>.<<.>++.++<                                              .[>]
//   /*P[*/P[p].b++;continue;}}}t=                                       stdout;
//  for (p=1;p<I;p++){/**/if(P[p].b>i                               ){i=P[p].b;}}
// if  (O){for(p=0;p<i;p++){q=0;/*[*/while(I               >++q){_=P[q].p-P[q ].q;
//b=   0;if(_<P[q ].o){while(012-*P[q].p)     {putchar(*(P[q].p++));b++;}P[q]. p++;
//}   ;while (P[  q].d>b++)putchar(040);}             putchar(10);}return 0;}p   =1;
//   for(;   p<I   ;p++)fwrite(P[p] .q,P[              p].o,1,t);return 0 ;}//
// #/*]     ]<.    [-]<[-]<[- ]<[    -]<               [-  ]<;*/elif  e    //b
// |(1        <<     ( __LINE__        /*               >>   `*//45))  |     01U
//            #                       /*               */     endif            //
namespace AutumnBox.Basic.Function.RunningManager
{
    using AutumnBox.Basic.Devices;
    using AutumnBox.Basic.Util;
    using AutumnBox.SharedTools;
    using System;
    /// <summary>
    /// 功能模块运行时托管器,一个托管器仅可以托管/包装一个功能模块,并只可以执行一次
    /// </summary>
    public sealed class RunningManager:BaseObject
    {
        /// <summary>
        /// 在功能模块中的命令执行器开始时,pid将会被赋值,以用于终止执行
        /// </summary>
        private int _pid;
        /// <summary>
        /// 构造!
        /// </summary>
        /// <param name="fm"></param>
        private RunningManager(FunctionModule fm)
        {
            this.Fm = fm;
            this.FuncEvents = new FuncEventsContainer(this);
            //绑定好事件,在进程开始时获取PID用于结束进程
            FuncEvents.ProcessStarted += (s_, e_) => { _pid = e_.PID; };
            Status = RunningManagerStatus.Loaded;
        }
        /*        PUBLIC         */
        /// <summary>
        /// 被托管的功能模块的各种事件
        /// </summary>
        public FuncEventsContainer FuncEvents { get; private set; }
        /// <summary>
        /// 当前托管器的状态
        /// </summary>
        public RunningManagerStatus Status { get; private set; } = RunningManagerStatus.Loading;
        /// <summary>
        /// 被托管的功能模块
        /// </summary>
        public FunctionModule Fm { get; private set; }
        /// <summary>
        /// 开始执行托管的功能模块
        /// </summary>
        public void FuncStart()
        {
            //检查完成事件是否被订阅
            if (!Fm.IsFinishEventBound) throw new EventNotBoundException();
            //检查是否已经被执行过一次
            if (this.Status != RunningManagerStatus.Loaded) throw new Exception("this Running Manager is finished,Please use new Running Manager");
            //绑定事件,在完成时将托管器设定为已完成
            Fm.Finished += (s, e) => { if(e.Result.IsSuccessful) Status = RunningManagerStatus.Finished; };
            Logger.D("FuntionIsFinish?", Fm.IsFinishEventBound.ToString());
            Status = RunningManagerStatus.Running;
            Fm.RunByRunningManager();
        }
        /// <summary>
        /// 强制停止执行管理的正在运行的功能
        /// </summary>
        public void FuncStop()
        {
            Fm.WasFrociblyStop = true;
            SystemHelper.KillProcessAndChildrens(_pid);
            Status = RunningManagerStatus.Cancel;
        }
        /// <summary>
        /// 无需设备连接实例即可创建功能模块托管器
        /// </summary>
        /// <param name="info"></param>
        /// <param name="fm"></param>
        /// <returns></returns>
        public static RunningManager Create(DeviceSimpleInfo info, FunctionModule fm)
        {
            fm.DevSimpleInfo = info;
            return new RunningManager(fm);
        }
    }
}
