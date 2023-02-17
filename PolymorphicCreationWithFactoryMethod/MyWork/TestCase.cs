namespace PolymorphicCreationWithFactoryMethod.MyWork
{
    public class TestCase
    {
    }

    public abstract class AbstractTestBuilder: TestCase
    {
        public OutputBuilder Builder { get; private set; }

        public abstract OutputBuilder GetBuilder();

        public void TestAddAboveRoot()
        {
            string invalidResult =
                "<orders>" +
                    "<order>" +
                    "</order>" +
                "</orders>" +
                "<customer>" +
                "</customer>";

            Builder = GetBuilder();

            Builder.AddBelow("order");

            try
            {
                Builder.AddAbove("customer");
                Fail("expecting RuntimeException");
            }
            catch (RuntimeException ignored)
            {

            }
        }

        private void Fail(string failureMessage)
        {
        
        }
    }
}