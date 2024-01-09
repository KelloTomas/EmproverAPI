using EmproverAPI.Models.DB;

namespace EmproverAPI.NUnitTest.Models.DB
{
    public class FunctionParameterValueTest
    {
        FunctionParameterValue testParameterValue = new() { ParameterDefinition = new() };
        private decimal maxValue = 99;
        private decimal minValue = 5;
        private decimal[] values = { 5, 55, 555 };
        private List<AllowedValues> allowedValues;

        [SetUp]
        public void Setup()
        {
            allowedValues = new()
            {
                new()
                {
                    Key = 1,
                    Value = values[0]
                },
                new()
                {
                    Key = 1,
                    Value = values[1]
                },
                new()
                {
                    Key = 1,
                    Value = values[2]
                }
            };
        }

        [Test]
        public void WhenValueInAllowedListAndInRange_ThenOK()
        {
            testParameterValue.ParameterDefinition.MinValue = minValue;
            testParameterValue.ParameterDefinition.MaxValue = maxValue;
            testParameterValue.ParameterDefinition.AllowedValues = allowedValues;

            testParameterValue.Value = values[0];

            Assert.IsTrue(testParameterValue.IsValid());
        }

        [Test]
        public void WhenValueInAllowedListAndNotInRange_ThenFail()
        {
            testParameterValue.ParameterDefinition.MinValue = minValue;
            testParameterValue.ParameterDefinition.MaxValue = maxValue;
            testParameterValue.ParameterDefinition.AllowedValues = allowedValues;

            testParameterValue.Value = values[2];

            Assert.IsFalse(testParameterValue.IsValid());
        }

        [Test]
        public void WhenNoAllowedListAndNotInRange_ThenFail()
        {
            testParameterValue.ParameterDefinition.MinValue = minValue;
            testParameterValue.ParameterDefinition.MaxValue = maxValue;

            testParameterValue.Value = maxValue + 1;

            Assert.IsFalse(testParameterValue.IsValid());
        }

        [Test]
        public void WhenNoAllowedListAndInRange_ThenOk()
        {
            testParameterValue.ParameterDefinition.MinValue = minValue;
            testParameterValue.ParameterDefinition.MaxValue = maxValue;

            testParameterValue.Value = maxValue;

            Assert.IsFalse(testParameterValue.IsValid());
        }

        [Test]
        public void WhenNoAllowedListAndOnRange_ThenOk()
        {
            testParameterValue.ParameterDefinition.MinValue = maxValue;
            testParameterValue.ParameterDefinition.MaxValue = maxValue;

            testParameterValue.Value = maxValue;

            Assert.IsFalse(testParameterValue.IsValid());
        }

        [Test]
        public void WhenAllowedListAndNoRange_ThenOk()
        {
            testParameterValue.ParameterDefinition.AllowedValues = allowedValues;

            testParameterValue.Value = values[1];

            Assert.IsTrue(testParameterValue.IsValid());
        }
    }
}