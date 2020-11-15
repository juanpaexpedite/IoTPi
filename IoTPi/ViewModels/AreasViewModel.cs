using Avalonia.Collections;
using IoTPi.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Avalonia.Threading;
using System.Threading.Tasks;

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

        bool adding = false;

        public async Task<AreaDescriptor> CheckArea(int id, string name)
        {
            while(adding)
            {
                await Task.Delay(1000);
            }

            if(!Collection.Any(a => a.Id == id))
            {
                var area = new AreaDescriptor() { Id = id, Name = name };
                adding = true;
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Collection.Add(area);
                    adding = false;
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
