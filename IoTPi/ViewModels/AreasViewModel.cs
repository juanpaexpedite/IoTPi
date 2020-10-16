using Avalonia.Collections;
using IoTPi.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Avalonia.Threading;

namespace IoTPi.ViewModels
{
    public class AreasViewModel : ViewModelBase
    {
        public static AreasViewModel Instance;

        public AreasViewModel()
        {
            Instance = this;
        }

        public AvaloniaList<AreaDescriptor> Collection { get; } = new AvaloniaList<AreaDescriptor>();

        public AreaDescriptor CheckArea(int id, string name)
        {
            if(!Collection.Any(a => a.Id == id))
            {
                var area = new AreaDescriptor() { Id = id, Name = name };
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Collection.Add(area);
                });
                return area;
            }
            else
            {
                return Collection.First(a => a.Id == id);
            }
            
        }
    }
}
