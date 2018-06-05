package mono.io.socket.emitter;


public class Emitter_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		io.socket.emitter.Emitter.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_call:([Ljava/lang/Object;)V:GetCall_arrayLjava_lang_Object_Handler:EngineIO.Emitter/IListenerInvoker, Engine.IO.Client\n" +
			"";
		mono.android.Runtime.register ("EngineIO.Emitter+IListenerImplementor, Engine.IO.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Emitter_ListenerImplementor.class, __md_methods);
	}


	public Emitter_ListenerImplementor ()
	{
		super ();
		if (getClass () == Emitter_ListenerImplementor.class)
			mono.android.TypeManager.Activate ("EngineIO.Emitter+IListenerImplementor, Engine.IO.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void call (java.lang.Object[] p0)
	{
		n_call (p0);
	}

	private native void n_call (java.lang.Object[] p0);

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
