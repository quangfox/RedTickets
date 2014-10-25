﻿using GalaSoft.MvvmLight.Command;
using Redmine.Portable.Interface;
using Redmine.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.ViewModel
{
    public class ProjectsViewModel : AsyncViewModelBase
    {
        private IDataService _dataService;

        public RelayCommand InitCommand { get; private set; }

        private List<Project> _projects;
        public List<Project> Projects
        {
            get { return _projects; }
            set { _projects = value; RaisePropertyChanged(); }
        }

        public ProjectsViewModel(IDataService dataService)
        {
            _dataService = dataService;

            InitCommand = new RelayCommand(Init);
        }

        private async void Init()
        {
            IsLoading = true;

            var result = await _dataService.GetProjects(0, Int16.MaxValue);
            if (result.IsSuccessStatusCode && result.Result != null && result.Result.Projects != null)
            {
                Projects = result.Result.Projects.ToList();
            }
            else
            { 
                // todo show error
            }

            IsLoading = false;
        }
    }
}