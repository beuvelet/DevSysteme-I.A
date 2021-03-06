using System;
using Conversive.DevBot5;

namespace DevBotConsoleApplication
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class DevBotConsoleApplication
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// DevBot variables
			DevBot5Engine DevBot = new DevBot5Engine();
			KnowledgeBase kb = new KnowledgeBase();
			KnowledgeBaseItem kbi = new KnowledgeBaseItem();
			State state = new State();

			// build the knowledgebase
			Rule vRule = new Rule();
			vRule.Id = kb.GetNewRuleId();
			vRule.AddInput("Hello", "");
			vRule.AddInput("Hi", "");
			vRule.AddOutput("Hello, World", "", "");
			kb.Rules.Add(vRule);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			// save the knowledgebase
			XMLToolbox xToolbox = new XMLToolbox(typeof(KnowledgeBase));
			xToolbox.SaveXML(kb, path + @"\kbi.vkb");

			// load the knowledgebase item
			kbi.Filename = "kbi.vkb";
			kbi.Fullpath = path + @"\";

			// set the knowledge base for DevBot
			DevBot.AddKnowledgeBase(kb, kbi);

			state.CurrentKBs.Add( path + @"\kbi.vkb");

			// get input
			Console.WriteLine("Please enter your message");

			while(true)
			{
				string msg = Console.ReadLine();
			
				// process the reply
				Reply reply = DevBot.GetReply(msg, state);
				if (reply != null)
					Console.WriteLine(reply.AgentText);
				else
					Console.WriteLine("No reply found.");
			}
		}
	}
}
