﻿using System;
using Microsoft.SharePoint.Client.Taxonomy;


namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    // public class TaxonomyItemPipeBind<T> where T : TaxonomyItem
    // {
    //     private readonly Guid _id;
    //     private readonly string _title;
    //     private readonly TaxonomyItem _item;

    //     public TaxonomyItemPipeBind(Guid guid)
    //     {
    //         _id = guid;
    //     }

    //     public TaxonomyItemPipeBind(string id)
    //     {
    //         if (!Guid.TryParse(id, out _id))
    //         {
    //             _title = id;
    //         }
    //     }

    //     public TaxonomyItemPipeBind(T item)
    //     {
    //         _item = item;
    //         _id = item.Id;
    //     }

    //     public Guid Id => _id;

    //     public string Title => _title;

    //     public T Item => _item as T;

    //     public TaxonomyItemPipeBind()
    //     {
    //         _id = Guid.Empty;
    //         _title = string.Empty;
    //         _item = null;
    //     }
    // }
}
