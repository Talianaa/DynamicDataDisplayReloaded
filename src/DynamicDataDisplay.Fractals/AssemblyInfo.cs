﻿using Microsoft.Research.DynamicDataDisplay;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

[assembly: XmlnsDefinition(D3AssemblyConstants.DefaultXmlNamespace, "Microsoft.Research.DynamicDataDisplay.Fractals")]
[assembly: Dependency("DynamicDataDisplay", LoadHint.Always)]
[assembly: Dependency("DynamicDataDisplay.Maps", LoadHint.Always)]
