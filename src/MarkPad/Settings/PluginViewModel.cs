﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using MarkPad.PluginApi;
using MarkPad.Services.Settings;
using MarkPad.Framework.Events;

namespace MarkPad.Settings
{
	public class PluginViewModel : PropertyChangedBase
	{
		readonly IPlugin _plugin;
		readonly IEventAggregator _eventAggregator;

		public PluginViewModel(
			IPlugin plugin,
			IEventAggregator eventAggregator)
		{
			_plugin = plugin;
			_eventAggregator = eventAggregator;
		}

		public string Name { get { return _plugin.Name; } }
		public string Version { get { return _plugin.Version; } }
		public string Authors { get { return _plugin.Authors; } }
		public string Description { get { return _plugin.Description; } }
		public bool CanOpenSettings { get { return _plugin.CanOpenSettings; } }

		public bool CanInstall { get { return !_plugin.Settings.IsEnabled; } }
		public void Install()
		{
			SetIsEnabled(true);
		}

		public bool CanUninstall { get { return _plugin.Settings.IsEnabled; } }
		public void Uninstall()
		{
			SetIsEnabled(false);
		}

		private void SetIsEnabled(bool isEnabled)
		{
			_plugin.Settings.IsEnabled = isEnabled;
			_plugin.SaveSettings();

			this.NotifyOfPropertyChange(() => CanInstall);
			this.NotifyOfPropertyChange(() => CanUninstall);
			_eventAggregator.Publish(new PluginsChangedEvent());
		}

		public void OpenSettings()
		{
		}
	}
}
