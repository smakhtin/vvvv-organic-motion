#region usings
using System;
using System.ComponentModel.Composition;

using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;

using VVVV.Core.Logging;
#endregion usings

namespace VVVV.Nodes
{
	#region PluginInfo
	[PluginInfo(Name = "OrganicMotion", Category = "Animation", Help = "Basic template with one value in/out", Tags = "")]
	#endregion PluginInfo
	public class AnimationOrganicMotionNode : IPluginEvaluate
	{
		#region fields & pins
		[Input("Time", DefaultValue = 1.0)]
		ISpread<double> FTimeIn;

		[Input("Motion Type")]
		ISpread<MotionType> FTypeIn;
		
		[Output("Equation")]
		ISpread<string> FEquationOut;
		
		[Output("Output")]
		ISpread<double> FValueOut;

		string[] FEquation = new string[15]
		{
			"sin(t)", "cos(t)", "cos(t)*sin(t)", "sin(t)*sin(t*1.5)", "sin(tan(cos(t)*1.2))", 
			"sin(tan(t)*0.05)", "cos(sin(t*3))*sin(t*0.2)", "sin(pow(8,sin(t)))", "sin(exp(cos(t*0.8))*2)", "sin(t-PI*tan(t)*0.01)",
			"pow(sin(t*PI),12)", "cos(sin(t)*tan(t*PI)*PI/8)", "sin(tan(t)*pow(sin(t),10))", "cos(sin(t*3)+t*3)", "pow(abs(sin(t*2))*0.6,sin(t*2))*0.6"
		};

		[Import()]
		ILogger FLogger;
		#endregion fields & pins

		//called when data for any output pin is requested
		public void Evaluate(int SpreadMax)
		{
			FValueOut.SliceCount = FEquationOut.SliceCount = SpreadMax;

			for (int i = 0; i < SpreadMax; i++)
			{
				var value = 0.0;
				var time = FTimeIn[i];

				switch(FTypeIn[i])
				{
					case MotionType.Type1:
						value = Math.Sin(time);
						break;
					case MotionType.Type2:
						value = Math.Cos(time);
						break;
					case MotionType.Type3:
						value = Math.Cos(time) * Math.Sin(time);
						break;
					case MotionType.Type4:
						value = Math.Sin(time) * Math.Sin(time * 1.5);
						break;
					case MotionType.Type5:
						value = Math.Sin(Math.Tan(Math.Cos(time) * 1.2));
						break;
					case MotionType.Type6:
						value = Math.Sin(Math.Tan(time) * 0.05);
						break;
					case MotionType.Type7:
						value = Math.Cos(Math.Sin(time * 3) * Math.Sin(time * 0.2));
						break;
					case MotionType.Type8:
						value = Math.Sin(Math.Pow(8, Math.Sin(time)));
						break;
					case MotionType.Type9:
						value = Math.Sin(Math.Exp(Math.Cos(time * 0.8) * 2));
						break;
					case MotionType.Type10:
						value = Math.Sin(time - Math.PI * Math.Tan(time) * 0.01);
						break;
					case MotionType.Type11:
						value = Math.Pow(Math.Sin(time * Math.PI), 12);
						break;
					case MotionType.Type12:
						value = Math.Cos(Math.Sin(time) * Math.Tan(time * Math.PI) * Math.PI / 8);
						break;
					case MotionType.Type13:
						value = Math.Sin(Math.Tan(time) * Math.Pow(Math.Sin(time), 10));
						break;
					case MotionType.Type14:
						value = Math.Cos(Math.Sin(time * 3) + time * 3);
						break;
					case MotionType.Type15:
						value = Math.Pow(Math.Abs(Math.Sin(time * 2)) * 0.6, Math.Sin(time * 2)) * 0.6;
						break;
				}

				FValueOut[i] = value;
				FEquationOut[i] = FEquation[(int) FTypeIn[i]];
			}
				
		}
	}

	public enum MotionType
	{
		Type1,
		Type2,
		Type3,
		Type4,
		Type5,
		Type6, 
		Type7,
		Type8,
		Type9,
		Type10,
		Type11,
		Type12,
		Type13,
		Type14,
		Type15
	}
}
