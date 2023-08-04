# TestProject WCF

Setup

- Visual Studio 2019
- .NET Framework 4.8

Dataflow

UI -> Proxy -> WcfService -> Repository -> Context -> DB

![TaskMgmt](./TaskMgmt.gif)

Version History
--

0.1.0

Known bugs

- UsageControl/TaskControl: Click on "Add new..." -> Delete button stays active (and if clicked: exception)
- WCF-timeout if UI not used for a while
- No data validation (for int)
- Error message When adding similar Usage (i.e. same Task-Material) - unspecified behaviour
- Exception when adding empty Task
- Exception when "Delete Usage Button" clicked after "New Usage Button" 
- If Material.Unit changed to a different base-unit - the related Usages.Units stay unchanged

Sample log of the Server (currenlty only DAL layer has)
--

	~\TestProject\TaskMgmt\TaskMgmt.Server\bin\Debug\logfile.txt
	
		(Currently only DAL Layer includes logging)

		2023-07-27 11:39:46.7588|I|T04|TaskMgmt.DAL.Repositories.TaskRepository|TaskRepository.GetTasks
		2023-07-27 11:39:46.8793|I|T04|TaskMgmt.DAL.Repositories.TaskMaterialUsageRepository|GetItems
		2023-07-27 11:39:54.0254|I|T04|TaskMgmt.DAL.Repositories.TaskRepository|TaskRepository.InsertTask ID=9229b7e5-d5c2-404f-ae2c-bf8e4a0d6d7f
		2023-07-27 11:39:54.0836|I|T04|TaskMgmt.DAL.Repositories.TaskRepository|TaskRepository.GetTasks
		2023-07-27 11:39:54.2692|I|T04|TaskMgmt.DAL.Repositories.TaskMaterialUsageRepository|GetItems
		2023-07-27 11:41:38.8425|I|T08|TaskMgmt.DAL.Repositories.TaskRepository|TaskRepository.DeleteTask ID=00000000-0000-0000-0000-000000000003
		2023-07-27 11:41:38.8914|I|T08|TaskMgmt.DAL.Repositories.TaskRepository|TaskRepository.GetTasks
		2023-07-27 11:41:39.0680|I|T08|TaskMgmt.DAL.Repositories.TaskMaterialUsageRepository|GetItems
		2023-07-27 11:41:51.2421|I|T10|TaskMgmt.DAL.Repositories.TaskRepository|TaskRepository.UpdateTask ID=00000000-0000-0000-0000-000000000002
		2023-07-27 11:42:02.4173|I|T03|TaskMgmt.DAL.Repositories.TaskRepository|TaskRepository.InsertTask ID=88e4f92e-3b84-4aed-82b2-dfb340ae29d5
		2023-07-27 11:42:02.4642|I|T09|TaskMgmt.DAL.Repositories.TaskRepository|TaskRepository.GetTasks
		2023-07-27 11:42:02.6262|I|T09|TaskMgmt.DAL.Repositories.TaskMaterialUsageRepository|GetItems
		2023-07-27 11:42:06.0401|I|T09|TaskMgmt.DAL.Repositories.TaskRepository|TaskRepository.DeleteTask ID=88e4f92e-3b84-4aed-82b2-dfb340ae29d5
		2023-07-27 11:42:06.0801|I|T09|TaskMgmt.DAL.Repositories.TaskRepository|TaskRepository.GetTasks
		2023-07-27 11:42:06.1195|I|T09|TaskMgmt.DAL.Repositories.TaskMaterialUsageRepository|GetItems

Sample log of the UI thread (only exceptions currently)

	~\TestProject\TaskMgmt\TaskMgmt.UI\bin\Debug\logfile.txt	
		2023-07-27 11:30:33.9923|E|T01|TaskMgmt.UI.App|System.NullReferenceException: Object reference not set to an instance of an object.
		   at TaskMgmt.UI.ViewModel.MainVM.AddRecord(Proxy proxy) in C:\~\TestProject\TaskMgmt\TaskMgmt.UI\ViewModel\MainVM.cs:line 134
		   at TaskMgmt.UI.ViewModel.MainVM.InvokeOnSelectedRecord(Action`1 action) in C:\~\TestProject\TaskMgmt\TaskMgmt.UI\ViewModel\MainVM.cs:line 120
		   at TaskMgmt.UI.ViewModel.MainVM.<HookUpUICommands>b__50_1(Object _) in C:\~\TestProject\TaskMgmt\TaskMgmt.UI\ViewModel\MainVM.cs:line 103
		   at TaskMgmt.UI.ViewModel.DelegateCommand.Execute(Object parameter) in C:\~\TestProject\TaskMgmt\TaskMgmt.UI\ViewHelper\DelegateCommand.cs:line 41
		   at MS.Internal.Commands.CommandHelpers.CriticalExecuteCommandSource(ICommandSource commandSource, Boolean userInitiated)
		   at System.Windows.Controls.Primitives.ButtonBase.OnClick()
		   at System.Windows.Controls.Button.OnClick()
		   at System.Windows.Controls.Primitives.ButtonBase.OnMouseLeftButtonUp(MouseButtonEventArgs e)
		   at System.Windows.UIElement.OnMouseLeftButtonUpThunk(Object sender, MouseButtonEventArgs e)
		   at System.Windows.Input.MouseButtonEventArgs.InvokeEventHandler(Delegate genericHandler, Object genericTarget)
		   at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target)
		   at System.Windows.RoutedEventHandlerInfo.InvokeHandler(Object target, RoutedEventArgs routedEventArgs)
		   at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised)
		   at System.Windows.UIElement.ReRaiseEventAs(DependencyObject sender, RoutedEventArgs args, RoutedEvent newEvent)
		   at System.Windows.UIElement.OnMouseUpThunk(Object sender, MouseButtonEventArgs e)
		   at System.Windows.Input.MouseButtonEventArgs.InvokeEventHandler(Delegate genericHandler, Object genericTarget)
		   at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target)
		   at System.Windows.RoutedEventHandlerInfo.InvokeHandler(Object target, RoutedEventArgs routedEventArgs)
		   at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised)
		   at System.Windows.UIElement.RaiseEventImpl(DependencyObject sender, RoutedEventArgs args)
		   at System.Windows.UIElement.RaiseTrustedEvent(RoutedEventArgs args)
		   at System.Windows.UIElement.RaiseEvent(RoutedEventArgs args, Boolean trusted)
		   at System.Windows.Input.InputManager.ProcessStagingArea()
		   at System.Windows.Input.InputManager.ProcessInput(InputEventArgs input)
		   at System.Windows.Input.InputProviderSite.ReportInput(InputReport inputReport)
		   at System.Windows.Interop.HwndMouseInputProvider.ReportInput(IntPtr hwnd, InputMode mode, Int32 timestamp, RawMouseActions actions, Int32 x, Int32 y, Int32 wheel)
		   at System.Windows.Interop.HwndMouseInputProvider.FilterMessage(IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
		   at System.Windows.Interop.HwndSource.InputFilterMessage(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
		   at MS.Win32.HwndWrapper.WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
		   at MS.Win32.HwndSubclass.DispatcherCallbackOperation(Object o)
		   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)
		   at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)
