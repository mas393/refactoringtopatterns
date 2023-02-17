﻿namespace PolymorphicCreationWithFactoryMethod.MyWork
{
    public class DOMBuilderTest: AbstractTestBuilder
    {
        //public OutputBuilder Builder { get; private set; }
        public override OutputBuilder GetBuilder()
        {
            return new DOMBuilder("orders");
        }

        /*
        public void TestAddAboveRoot()
        {
            string invalidResult =
                "<orders>" +
                    "<order>" +
                    "</order>" +
                "</orders>" +
                "<customer>" +
                "</customer>";

            Builder = new DOMBuilder("orders");

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
        */
    }
}
