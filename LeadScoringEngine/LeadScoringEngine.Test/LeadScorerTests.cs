using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LeadScoringEngine.Test
{
    [TestClass]
    public class LeadScorerTests
    {
        private LeadScorer target;

        [TestInitialize]
        public void InitializeTest()
        {
            target = new LeadScorer();
        }

    }
}
