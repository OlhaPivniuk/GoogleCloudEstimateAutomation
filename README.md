Use Task as a precondition for the current task, in which you need to develop a framework based on it, which will include the following features:

1. A WebDriver manager for managing browser connectors
2. Page Object/Page Factory for page abstractions
3. Models for business objects of the required elements
4. Property files with test data for at least two different environments
5. If the test fails, a screenshot with the date and time is taken.

Please find the precondition description below and provide the link to your solution in the Answer field:
Precondition: 
Automate the following script:
1. Open https://cloud.google.com/.
2. Click on "Add to estimate" button.
3. Select "Compute Engine".
4. Fill out the form with the following data:
* Number of instances: 4
* Operating System / Software: Free: Debian, CentOS, CoreOS, Ubuntu or BYOL (Bring Your Own License)
* Provisioning model: Regular
* Machine Family: General purpose 
* Series: N1 
* Machine type: n1-standard-8 (vCPUs: 8, RAM: 30 GB)
* Select “Add GPUs“
* GPU type: NVIDIA V100
* Number of GPUs: 1
* Local SSD: 2x375 Gb
* Region: Netherlands (europe-west4)
Other options leave in the default state.
5. Click "Share" to see Total estimated cost
6. Click "Open estimate summary" to see Cost Estimate Summary, will be opened in separate tab browser.
7. Verify that the 'Cost Estimate Summary' matches with filled values in Step 4.
