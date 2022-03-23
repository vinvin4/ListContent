# ListContent
version 1.1:
Project reworked as per feedbacks;
1. Core-part - Significantly reworked services-flow. (All changes and my knowledges can be found in git-history); Removed separated models for fact-details. Downloading content process moved inside the Core as much as possible;
2. ContentList - renamed to ContentList.Android - Cleaned fragments functionality due the reworking services-flow. Adapted a bit UI. Significantly reworked UI-flow during content downloading. Reworked flow to load pictures and added several handlers to predict using project on low OS-versions;
3. CoreTest - reworked a bit tests. Completely removed test to check ZooAnimalt-service workability, because integrated translations corrupts testing;

Tested:
1. Xiaomi Redmi 6A (OS: Android 9);
2. Samsung Galaxy S6 (OS: Android 7);
3. Samsung Galaxy J1 (OS: Android 5.1.1);

<!-- -->
version 1.0:
Project initialized. 3 parts are implemented
1. Core - crossplatform part. Contains general flows to receive information from web-services and adapting these for using on native-side. There are available 3 different types of serices, 3 different API-groups. Plus, 1 more, a bit hardcode service to provide predefined information about author (me);
2. ContentList - Android native part. Contains flows to adapt received information in UI. No business-logic included, all information are getting from Core-part;
3. CoreTest - functional tests for Core-project;
4. Android Test - Is not implemented due to equipment problems. Initially, it was not initialized for job, unfortunately;

Tested:
1. Xiaomi Redmi 6A, (OS: Android 9);
