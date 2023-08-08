﻿#if ANDROID || (IOS && !MACCATALYST)
using System;
using System.Collections.Generic;

namespace Microsoft.Maui.Controls
{
	partial class HideSoftInputOnTappedChangedManager
	{
		IDisposable? _watchingForTaps;
		WeakReference<IView>? _focusedView;

		internal void UpdatePage(ContentPage page)
		{
			if (page.HideSoftInputOnTapped && page.HasNavigatedTo)
			{
				if (!_contentPages.Contains(page))
					_contentPages.Add(page);
			}
			else
			{
				if (_contentPages.Contains(page))
					_contentPages.Remove(page);
			}

			SetupHideSoftInputOnTapped();
		}

		internal IDisposable? UpdateFocusForView(IView _view)
		{
			// Update to new focused view
			if (_view.IsFocused)
			{
				DisconnectFromPlatform();
				_focusedView = new WeakReference<IView>(_view);
			}
			// If currently tracked view became unfocused then disconnect from it
			else if (_view == FocusedView)
			{
				DisconnectFromPlatform();
				_focusedView = null;
			}

			if (!FeatureEnabled)
			{
				DisconnectFromPlatform();
				return null;
			}

			if (_view is not VisualElement ve)
				return null;

			if (!ve.IsLoaded || !_view.IsFocused || ve.Window is null)
				return null;

			DisconnectFromPlatform();
			IDisposable? platformToken = SetupHideSoftInputOnTapped((_view.Handler as IPlatformViewHandler)?.PlatformView);
			ve.RemovedFromPlatformVisualTree += OnFocusedViewRemovedFromPlatformTree;

#if ANDROID
			var window = ve.Window;
			window.DispatchTouchEvent += OnWindowDispatchedTouch;
#endif
			_watchingForTaps = new ActionDisposable(() =>
			{
				platformToken?.Dispose();
				platformToken = null;
#if ANDROID
				window.DispatchTouchEvent -= OnWindowDispatchedTouch;
				window = null;
#endif
				ve.RemovedFromPlatformVisualTree -= OnFocusedViewRemovedFromPlatformTree;
			});

			return _watchingForTaps;
		}

		void OnFocusedViewRemovedFromPlatformTree(object? sender, EventArgs e) =>
			DisconnectFromPlatform();

		void DisconnectFromPlatform()
		{
			_watchingForTaps?.Dispose();
			_watchingForTaps = null;
		}

		IView? FocusedView
		{
			get
			{
				if (_focusedView?.TryGetTarget(out IView? view) == true)
				{
					return view;
				}

				return null;
			}
		}
		internal void SetupHideSoftInputOnTapped()
		{
			if (FocusedView is not null)
			{
				UpdateFocusForView(FocusedView);
			}
		}
	}
}
#endif