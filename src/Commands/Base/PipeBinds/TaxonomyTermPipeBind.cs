﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;


namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class TaxonomyTermPipeBind
    {
        private readonly Guid _id;
        private readonly string _title;
        private readonly Term _item;

        public TaxonomyTermPipeBind(Guid guid)
        {
            _id = guid;
        }

        public TaxonomyTermPipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                _title = id;
            }
        }

        public TaxonomyTermPipeBind(Term item)
        {
            _item = item;
            _id = item.Id;
        }

        public Guid Id => _id;

        public string Title => _title;

        public Term Item => _item;

        public Term GetTerm(ClientContext clientContext, TermStore termStore, TermSet termSet, bool recursive, Expression<Func<Term, object>>[] expressions = null)
        {
            Term term = null;
            if (_id != Guid.Empty)
            {
                term = termStore.GetTerm(_id);
            }
            else if (!string.IsNullOrEmpty(_title) && termSet != null && termStore != null)
            {
                var termName = TaxonomyExtensions.NormalizeName(_title);
                if (!recursive)
                {
                    term = termSet.Terms.GetByName(termName);
                }
                else
                {
                    var lmi = new LabelMatchInformation(clientContext)
                    {
                        TrimUnavailable = true,
                        TermLabel = termName
                    };

                    var termMatches = termSet.GetTerms(lmi);
                    clientContext.Load(termMatches);
                    clientContext.ExecuteQueryRetry();

                    if (termMatches.AreItemsAvailable)
                    {
                        term = termMatches.FirstOrDefault();
                    }
                }
            }
            else
            {
                throw new PSArgumentException("Not enough parameters specified to succesfully find the term");
            }
            if (expressions != null)
            {
                clientContext.Load(term, expressions);
            }
            else
            {
                clientContext.Load(term);
            }
            clientContext.ExecuteQueryRetry();
            return term;
        }

        public TaxonomyTermPipeBind()
        {
            _id = Guid.Empty;
            _title = string.Empty;
            _item = null;
        }
    }
}
