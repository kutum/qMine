using System;

namespace MinecraftServerRCON
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public sealed class RCONMessageType
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    {
		private int type = -1;

		private RCONMessageType(int type)
		{
			this.type = type;
		}
		
		public int Value
		{
			get
			{
				return this.type;
			}
		}
		
	    public static bool operator ==(RCONMessageType x, RCONMessageType y) 
	    {
	    	if(Object.Equals(x, null))
	    	{
	    		return false;
	    	}
	    	
	    	if(Object.Equals(y, null))
	    	{
	    		return false;
	    	}
	    	
	    	return x.Value == y.Value;
	    }
	    
	    public static bool operator !=(RCONMessageType x, RCONMessageType y) 
	    {
	    	if(Object.Equals(x, null))
	    	{
	    		return true;
	    	}
	    	
	    	if(Object.Equals(y, null))
	    	{
	    		return true;
	    	}
	    	
	    	return x.Value != y.Value;
	    }
	    
	    public static bool operator ==(RCONMessageType x, int y) 
	    {
	    	if(Object.Equals(x, null))
	    	{
	    		return false;
	    	}
	    	
	    	return x.Value == y;
	    }
	    
	    public static bool operator !=(RCONMessageType x, int y) 
	    {
	    	if(Object.Equals(x, null))
	    	{
	    		return true;
	    	}
	    	
	    	return x.Value != y;
	    }
		
		public static readonly RCONMessageType Response = new RCONMessageType(0);
		public static readonly RCONMessageType Command = new RCONMessageType(2);
		public static readonly RCONMessageType Login = new RCONMessageType(3);
		public static readonly RCONMessageType Invalid = new RCONMessageType(-1);
	}
}
