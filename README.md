## 🛠 Task Description

You need to build a simple suggestion algorithm. 

### Context:

Our business is a **weekly, subscription-based meal box company**. Active subscribers can select from a rotating menu of dishes, including both main meals and breakfasts. Each subscriber has an **allocation** (e.g., 5 large main meals and 3 breakfasts) to distribute as they like.  

If a subscriber does not make a selection by the payment deadline, we automatically pick for them using a suggestion algorithm. The goal is to generate suggestions that the subscriber will likely be happy with. "Happy" is a vague term which is open for you to interpret.

An example implementation is provided in `ExampleChoiceSuggestionService`. Feel free to either refactor/improve upon it or start from scratch.

### Criteria

The criteria is totally up to you to decide! We recommend you check out the `Model explanation` section for some ideas of different criteria to include. Try to aim for at least 4 levels of criteria, ranging from high priority to low priority.

### Requirements:
- Implement the `IChoiceSuggestionService` interface in the provided `ChoiceSuggestionService`.
- Ensure the Criteria under the `Criteria` section of this README.md is implemented.
- Complete the unit tests provided within `SimmerTests` project or feel free to remove the scaffolded tests and add your own. They are just a base to help you get started.


### Model explanation

Here is a breif overview of the models which exist within the test. You can use a combination of these to build your priority list.

#### Diet

PLEASE NOTE: subscribers should NOT be allocated any menu items which do not match either their Diet or which includes any of their Blocked Ingredients.

`SubscriptionContext.DietaryPreferences.Diet` has options of 

- Everything
- Vegan
- Vegetarian
- MeatOnly

We need to ensure that subscribers only are assigned meals which are suitable for their diet. Within the example service, you can see how the method `AllowedByDiet()` handles the different diet preferences. Feel free to use this or adapt as you wish.

#### Blocked Ingredients

Blocked Ingredients are typically allergies or ingredients which the subscriber has told us they do not want suggestions from. We need to ensure when running the algorithm, that any menu items which contain the blocked ingredients, are filtered out from the suggestions.

You can find the blocked ingredidents for a subscriber at: `SubscriptionContext.DietaryPreferences.BlockedIngredientIds[]`. The MenuItem class has an IngredientIds collection you can compare against. 

#### Recommended Items

Business Recommended Menu Items. Recommended Menu Items are typically best sellers and menu items the business would be keen to push on a particular week. These can be found within `MenuContext.RecommendedMenuItems[]` which is a collection of menu item IDs. 

#### Subscribers previous orders

The subscribers previously ordered menu items. `SubscriptionContext.PreviouslyOrderedMenuItems[]`

#### Subscribers menu item reviews

The subscribers reviewed menu items. The data for the reviews can be found in `SubscriptionContext.LatestReviews[]`. The `MenuItemReview` class has a Score property which is assessed as 1-5, with 1 being Poor and 5 being Excellent.

---

## ✅ Evaluation Criteria

We will assess your submission based on:
- **Implementation of criteria**
- **Code quality & structure**
- **Correctness & completeness**
- **Performance considerations**
- **Error handling & edge cases**
- **Test coverage**

Bonus points for:
- Clean and idiomatic code

---

## 📖 Additional Notes

- Please include a `NOTES.md` file which details the criteria orderinif you want to explain any design choices or assumptions.
- If you have any questions, reach out to engineering@simmereats.com

Good luck, and happy coding! 🚀
