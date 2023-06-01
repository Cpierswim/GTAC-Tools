using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AddressException
/// </summary>
[Serializable]
public class AddressException : Exception
{
	public AddressException(String Message) : base(Message)
	{
        
	}
}