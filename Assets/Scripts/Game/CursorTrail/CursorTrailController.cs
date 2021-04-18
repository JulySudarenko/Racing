﻿using Tools;
using UnityEngine;

namespace Game.CursorTrail
{
    internal class CursorTrailController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/trailCursor"};
        private CursorTrailView _view;
        
        public CursorTrailController()
        {
            _view = LoadView();
            _view.Init();
        }

        private CursorTrailView LoadView()
        {
            GameObject objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObjects(objView);
            return objView.GetComponent<CursorTrailView>();
        }
    }
}
