using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIToolRemake
{
    public class Scenario
    {
        private int _scenarioID = 0;
        private string _scenarioName = "";
        private float _scoreMultiplier = 0f;
        private float _constant = 0f;
        private string _author = "";
        private int _feature = 0;//[Buster][Arts][Study][Balanced]
        private string _package = "";//packageid;
        public int ScenarioID
        {
            get => _scenarioID;
            set
            {
                if (_scenarioID != value)
                {
                    _scenarioID = value;
                }
            }
        }
        public string ScenarioName
        {
            get => _scenarioName;
            set
            {
                if (_scenarioName != value)
                {
                    _scenarioName = value;
                }
            }
        }
        public float ScoreMultiplier
        {
            get => _scoreMultiplier;
            set
            {
                if (value != _scoreMultiplier)
                {
                    _scoreMultiplier = value;
                }
            }
        }
        public float Constant
        {
            get => _constant;
            set
            {
                if (value != _constant)
                {
                    _constant = value;
                }
            }
        }
        public string Author
        {
            get { return _author; }
            set
            {
                if (value != _author)
                {
                    _author = value;
                }
            }
        }
        public int Feature
        {
            get { return _feature; }
            set
            {
                if (value != _feature)
                {
                    _feature = value;
                }
            }
        }
        public string Package
        {
            get { return _package; }
            set
            {
                if (value != _package)
                {
                    _package = value;
                }
            }
        }
    }

}
