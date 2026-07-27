namespace InventoryManagementAssignment3.Controller
{
    /// <summary>
    /// Coordinates operations between the UI/View and the Service layer.
    /// </summary>
    internal class MainController
    {
        private const int MinTaskChoice = 1;
        private const int MaxTaskChoice = 4;

        /// <summary>
        /// Processes the selected menu option and executes the corresponding controller task.
        /// </summary>
        /// <param name="choice">The user's menu selection.</param>
        /// <returns>true to continue displaying the menu; false to exit.</returns>
        public bool HandleMenu(int choice)
        {

            switch (choice)
            {
                case 1:
                    this.AddProduct();
                    break;
                case 5:
                    this.PrintGoodBye();
                    return true;
            }
        }

        public void AddProduct()
        {
            this._view
        }
    }
}
