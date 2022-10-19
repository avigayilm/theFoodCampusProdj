

namespace theFoodCampus.Models
{
        public enum MeasurementType { Cup, TableSpoon, Teaspoon, Grams, Pinch,Ounces, None }
        public enum Kashrut { Dairy, Pareve, Meaty }
        public enum Difficulty { None,Easy, Intermediate, Difficult }
        public enum PrepTime {None, Quick, Medium, Long }
        public enum Holiday { None,Rosh_Hashana, Yom_Kippur, Succot, Shmini_Atzeret, Hannuka, Tubishvat, Purim, Pesach, Shavouot, Lagbaomer }
        public enum Event { None,Exams, Vacation, Beginning_Of_Semester, End_Of_Semester, Project, Occasion }
        public enum Weather { None,Hot, Regular, Cold }
        public enum Category {None, Soup, Salad, Desert, CakeAndCookies, Breads, Fish, Chicken, Meat, Quiche, Pasta,SideDish }
        public enum Diet {None, GlutenFree, Vegeterian, Healthy }
        public enum Budget {None, Cheap, Affordable, Expensive }
}
