package md51bd8e6c8284354ae28d348b44543b38a;


public class NotesListItems_MyBroadcastReceiver
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("Airlink.NotesListItems+MyBroadcastReceiver, Airlink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NotesListItems_MyBroadcastReceiver.class, __md_methods);
	}


	public NotesListItems_MyBroadcastReceiver () throws java.lang.Throwable
	{
		super ();
		if (getClass () == NotesListItems_MyBroadcastReceiver.class)
			mono.android.TypeManager.Activate ("Airlink.NotesListItems+MyBroadcastReceiver, Airlink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
