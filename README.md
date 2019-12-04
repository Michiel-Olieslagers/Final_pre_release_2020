#Food Magnate Simulation
Food Magnate Simulation is a program that lets the user set up a number of food companies and a
settlement and see how well each company performs over time.

**Each company will be either:**
- a fast food restaurant chain
- a family restaurant chain or
- a named-chef restaurant chain.

The different types of restaurant chain have the same set of properties but have different starting
values for these properties; for instance the average cost of making a meal for a named-chef chain is
higher than for a fast food chain and a named-chef chain has a higher starting value for its reputation.

Each company has a number of outlets from which potential customers can purchase a meal.

A settlement contains a collection of households. For each household there is a probability that
indicates how likely it is that one person from that household will eat out (ie eat at an outlet owned by
one of the food companies) on any given day.
The end of a day is indicated each time the simulation is advanced. The program checks to see if a
household ate out and, if they did, it then randomly selects the company they used. Companies with
a higher reputation are more likely to be selected. Once a company has been selected, the program
then calculates which of that company’s outlets is the nearest to that household and adds one to the
number of visits for that day for that outlet. The total number of visits is used to determine the
income from meals sold and, together with various costs for the outlets and company, the profit/loss
for the company that day.

Each time the simulation is advanced there is a small chance that one or more events could occur.

**The potential events are:**
- the building of new households in the settlement
- a change in fuel cost for a company
- a change in daily costs for the company (daily costs include items like wages for employees, rent
and bills – although these are not specified in the simulation and are all just included in the daily
costs figure)
- a change in the average cost of a meal (the cost of producing the meal) for a company
- a change in reputation (a company’s reputation may go up due to reasons such as a good review
by a critic or may go down due to reasons such as a food poisoning outbreak or it being discovered
by the public that the fat-free yoghurt they sell is not actually fat-free).

**Every settlement has a:**
- size determined by X and Y values
- starting number of households
- collection of households.

**Every household has a:**
- unique ID
- location within a settlement given by coordinates
- probability that one person from that household will eat out on any given day.

**Every company has:**
- a name
- a category (fast food, family or named-chef)
- a balance (the amount of money currently in its bank account)
- a reputation score (indicates how well respected the company is; the better respected a company
is, the more likely it is that they will be chosen by potential customers)
- an average cost of making a meal
- an average price at which they sell a meal
- a daily cost (the fixed costs for the company irrespective of how many customers they have)
- a fixed delivery cost (the cost of getting the goods they need each day delivered, irrespective of
how many outlets they own)
- a fuel cost per unit (used to calculate the cost of delivering to additional outlets)
- capacity information used to calculate the starting capacity for outlets of different categories; the
capacity determines the number of people per day who can eat at that outlet
- cost information for the different categories used to calculate the cost of opening a new outlet of
that category
- a collection of outlets.

When a new simulation is created, the user is given the choice of creating a normal settlement, which
has a size of 1000 x 1000 and 250 households, or a large settlement where the user can specify how
much larger to make the settlement and how many additional households to create. They are then
given a choice of starting with a collection of default companies or to create their own companies.
