using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[CollectionDataContract(Name = "ArrayOfString", Namespace = "http://oplmanager.no-ip.info/", ItemName = "string")]
public class ArrayOfString : List<string>
{
}
