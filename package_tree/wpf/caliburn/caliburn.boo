install caliburn:
	description "Designed to aid in the development of WPF and Silverlight applications, Caliburn implements a variety of UI patterns for solving real-world problems. Patterns that are enabled by the framework include MVC, MVP, Presentation Model (MVVM), Commands and Application Controller."
	get_from svn("https://caliburn.svn.codeplex.com/svn")
	build_with nant, buildfile("default.build"), FrameworkVersion35	
		
	switches:
		parameters "skip.tests=true"

	with:
		tasks release,platformNet35

	shared_library "lib/net-3.5"
	build_root_dir "bin/net-3.5/release"		
	
dependencies:
	depend "castle" >> "Castle.Core"
	depend "castle" >> "Castle.DynamicProxy2"
	depend "castle" >> "Castle.MicroKernel"
	depend "castle" >> "Castle.Windsor"
	
package.homepage = "http://caliburn.codeplex.com/"
package.forum    = "http://caliburn.codeplex.com/Thread/List.aspx"