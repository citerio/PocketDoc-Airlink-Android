package md51bd8e6c8284354ae28d348b44543b38a;


public class NoteDetails
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Airlink.NoteDetails, Airlink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NoteDetails.class, __md_methods);
	}


	public NoteDetails () throws java.lang.Throwable
	{
		super ();
		if (getClass () == NoteDetails.class)
			mono.android.TypeManager.Activate ("Airlink.NoteDetails, Airlink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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