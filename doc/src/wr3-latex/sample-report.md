# WR3 Sample OCR Transcript

This transcript was generated from classroom photos of the WR3 sample report.
It preserves page order where possible and keeps detected tables inline in Markdown.
The numbered headings below follow OCR grouping order from the photos and should be treated as reconstructed sequence markers, not guaranteed original PDF page numbers.

## Page 1 of 46 — j OOS SN SN AARLAS CLULCTG 3

Source images: `IMG_20260414_160554.jpg`

j OOS SN SN AARLAS CLULCTG 3 1 Development Strategy]... 2... 3

### 1.1.1 Waterfull Model. 2. 3

L1.2 Agile Mod....

### 1.2 Progress and Deliverables ee:

### 1.3 Challenges and Testing Methods................ wee. )§ G6] [=]

2 Test Strategy Qe

### 2.1 Testing Objectives 2... eee ee. 9

### 2.1.1 Verification ©... eee ee ee. 9

ZA.2 Validation. 2... eee ee ll

### 2.2 CTY PCS is aie = wisi ES ww. dE OE Gee new ce cmc ud uc 1]

### Bo Unit hstiig ae eee ll

2:22 Integration [tinge eS sei 22:5 System [stiigee yaad

### 2.3 Testing Environment................2000-000-. JA

### 2.3.1 Unity Engine Version...................... 14

### 2.3.2 Test Data Preparation 2... eee 15

### 2.4 Regression Strategy... 2. ee ee ee ee ee ees 1G

### 2.4.1 Smoke Testing..............0..2.2.02022-22-. 16

### 2.4.2 Regression Testing.......0..0....00002.2222. 16

### Kod 3 Test Plan 17

## Page 2 of 46 — 1.1.1 Waterfull Model

Source images: `IMG_20260414_160620.jpg`

=F,

### 1.1.1 Waterfull Model

At the beginning of the project, we considered using the Waterfall Model, which igns well with the structure of the weckly reports. As shown in Figure 1.1, this F nodel involves clearly defined phases: requirements gathering, system design, pro- [a], totyping, implementation, testing, and deployment It. appeared to be a safe and structured approach, especially suitable for team members less experienced with Conepe 26h programming or the software development process.

### Eanes

definition =, i

### See Si

integration and system testing

### Operation and

ag Watt sacra 8 CONE bd at Fal

## Page 3 of 46 — Figure 1.2: Agile Development [1] ‘oF

Source images: `IMG_20260414_160625.jpg`

Figure 1.2: Agile Development [1] ‘oF yom the outset, Runhan primarily served as the main developer. Mcan- Sa : tracking development. progress, facilitating team communication, and refining requirements based on continuous feedback. While the other team members were less involved in direct. coding, they made essential contributions to testing, documentation, report writing, and overall coordination. Figure 1.3 below illustrates the roles and interactions among team members during the development process. Our development process followed a lightweight Agile approach tailored to our lining current core features and gameplay ideas. Based on these specifications, Runhan would implement a basic, functional version of the game. Although these carly versions were not fully refined, they served as concrete prototypes that allowed for casy review and evaluation. Once a version was delivered, the project managers reviewed the implementaai tion in Unity, identified areas for improvement, and provided targeted feedback.

## Page 4 of 46 — Frequent builds for

Source images: `IMG_20260414_160635.jpg`

; as

### Frequent builds for

feedback: o ~eD. =m « a i Bee wot,- 4 2: re COMAPE ATs

### ST / PMs send updates Se Sake kt

: to developer Project \ vw A = Developer Manager L£E> provides builds ideas, conduct for testing reports

### QA tests delivered

builds and reports bugs A» on > Refine and share at sl = Specs, update team " & = - J "J @ on

### Longyu Yaqiao Qiuyang

### Researcher / Tester / Writer / Coordinator

4 re ea Tar “ e° eral ‘ va ‘ s eat 3? ‘

## Page 5 of 46 — 1.2 Progress and Deliverables

Source images: `IMG_20260414_160719.jpg`

### 1.2 Progress and Deliverables

We organised our development around three main deliverables, cach aligned with key milestones in the project timeline. These deliverables helped structure the workflow and provided clear targets for planning, testing, and evaluation. of

### COtAP4135/5

1. v1.0: The first deliverable was completed around the time of Weekly Report

2, during which the initial prototype was finalised. At this stage, the game evolved from a basic demo into a version that reflected the intended design and visual style, based on our carly wireframes and concept drafts. Although some runtime issues remained, the prototype was functional and demonstrated the core gameplay and user interface, allowing for carly feedback and iteration.

2. v2.0: The second deliverable followed Weekly Report 3 (the current stage),

after a round of internal testing. As discussed in Chapter 3, team members conducted systematic test plans to ensure that the game was not only functional but also free from major bugs within the defined testing scope. This stage focused on refining the gameplay mechanics, improving UI responsiveness, and optimising performance. The feedback collected during this phase directly informed the subsequent development decisions. : 3. v3.0: The final deliverable coincided with the project submission deadline. By this point. all intended features had been implemented. the user interface

## Page 6 of 46 — on the project’s GitLab repository page: https://csprojects.nottingham.edu.. a

Source images: `IMG_20260414_160733.jpg`

All three stages have been or will be tagged in GitLab. These tags are available ea on the project’s GitLab repository page: https://csprojects.nottingham.edu.. a tags record is shown in Figure 1.4 below. ale

### 1.3 Challenges and Testing Methods. {couvassis

### Instead of following a conventional Test-Driven Development (TDD) approach, we

encountered several practical challenges that made it difficult to fully adopt this methodology, despite working within an Agile framework. First, writing automated tests in Unity presents notable limitations. Unlike traditional backend or frontend environments, where unit testing is straightforward and well-supported, Unity’s component-based architecture and reliance on real-time rendering, event-driven behaviour, and scenc-based interaction make it challenging to isolate logic for testing. Many gameplay behaviours are tightly coupled with the game engine lifecycle (c.g., Update(), Start (), OnTriggerEnter()), which are difficult to simulate through simple unit tests. As a result, writing meaningful and effective tests often requires additional effort to decouple code or rely on Unity’s own

## Page 7 of 46 — “eee eee FP Ow eS Fee eww ertet 8 eet em Sltg wt! elie }

Source images: `IMG_20260414_160753.jpg`

“eee eee FP Ow eS Fee eww ertet 8 eet em Sltg wt! elie }

- Najor bugs from v1.6 identified and resolved

- Testing feedback incorporated into ongoing development

© v1.0 ve v1. vifel Pa8eaScB - Merge branch ‘Qiuyang_LongyuUnittest’ into ‘main’ > 1 day ogo ee 7: u

- Basic UI implemented, closely following the original prototype oF

design

- Core gameplay structure established espana

~ Demonstrates overall design concept and intended style Ry

- Some runtime issues and bugs present, but the game is functional

for review and feedback Figure 1.4: Tags Record testing tools, such as the Unity Test Framework (UTF), which still imposes a learning curve and sctup complexity for our developers with limited testing experience Second, our development process was characterised by rapid and frequent changes in requirements. As the game was being itcratively designed and improved based on internal and external feedback, it was essential to provide quick and visible progress Lo the project managers (PMs) in cach development cycle. In such a context, manually running the game and reviewing its behaviour directly provided faster and ea ~~; e,e r 2 y «tt e,°e z e ¢@6 * ‘ ‘ * » rvya iB)

## Page 8 of 46 — that cach module performs as expected and that their interactions produce the

Source images: `IMG_20260414_160825.jpg`

that cach module performs as expected and that their interactions produce the intended outcomes [I]. In our project, the following aspects were subject to verification to ensure that [a] the system Was implemented correctly and reliably: [

### COMPSI19S

### Validate Core Gameplay

e Snake Logic: Ensure the snake’s movement, growth (Grow method), state resct (ResetState method), and collision detection work as expected. For example, in Snake.cs, verify that the direction is controlled by player input (W, A, S, D) and the positions of cach node are updated in FixedUpdate. e Food Generation and Interaction: Check that the method of RandomizePositionAndColor in Food.cs correctly randomizes food positions and selects random colors—while ensuring the color matches the displayed [english word, guarantecing the UI text shows the correct word. e Fake Food Logic: For FakeFood.cs, ensure that when updating the color, be the selected color always differs from the real food’s color. Also, verify the fa on pe score deduction and crror prompts upon collision. ae VJaliadnéa Blraan TFatamtnan ana Tatananrtian

## Page 9 of 46 — EE EEIINEY SS MAMIGTS WOM UHC real [oaod’s color. Also, vorify the i )

Source images: `IMG_20260414_160834.jpg`

EE EEIINEY SS MAMIGTS WOM UHC real [oaod’s color. Also, vorify the i ) score deduction and error prompts upon collision. ° l

### Validate User Interface and Interaction

e Menu and Transitions: Test the interface transitions in [m] StartMenuManager.cs, WelcomeMenu.cs, and InstructionMenu.cs to onsure correct screen changes after button clicks, allowing the user to smoothly — "4 proceed to the game or next sercen. e Pause and Exit Mechanism: Verify that the pause, restart, and exit funetions in PauseManager.cs and Pause.ExitManager.cs work as intended.

### Correctness of Educational Functionality

The food component is not only part of the game logic but also serves an educational purpose by randomly displaying English words and corresponding colors. Testing should focus on: e Whether the word matches the color.: e Whether sound effects play correctly (when audio resources are available). ir ow e Whether the player receives correct positive feedback (score increase) or negative feedback (score deduction) upon collision with food.

## Page 10 of 46 — implemented core functionalities (snake movement, food randomization, UI transi-

Source images: `IMG_20260414_160852.jpg`

### After cach modification, conduct automated regression testing Lo ensure previously

implemented core functionalities (snake movement, food randomization, UI transi- Lions, scoring logic, etc.) remain intact.: ie

### 2.1.2 Validation of

Compared to verification, validation is broader in scope and focuses i ensuring § =—corapaiss, that the software meets the expectations and needs of its stakeholders. It addresses whether the system fulfils tts intended purpose and delivers value to its users [1]. [In our project, two three methods were uscd for validation: 1, We maintained regular communication with one of our key stakcholders: the lecturers, by sharing our design concepts and development progress. The feedback received helped ensure that our project remained aligned with its intended purpose: creating an educational game suitable for children aged 5 lo 10.

2. The Project Managers conducted frequent stand-up mectings with the devel-

oper to exchange ideas and discuss evolving requirements. These mectings enabled us to adapt quickly to changes, clarify design intentions, and validate whether new features aligned with the overall project goals and stakcholder expectations. (Nene auc kim a lsk An wena ncect) inalsmuncimw wannaualh ta waelmex anc le! mA <col

## Page 11 of 46 — RQ ONTETEE MATTE AWA, GOTIVONIL, ie

Source images: `IMG_20260414_160900.jpg`

### RQ ONTETEE MATTE AWA, GOTIVONIL, ie

### 2.2 Test Types of

In this project, our team employed multiple testing types to comprehensively cover —.°%"5/ all aspects of the system: b

### 2.2.1 Unit Testing

Unit testing is a software testing method where individual components or functions of a program are tested in isolation to ensure they perform as expected (1). In this project, we implemented extensive unit ests) using Unity Test Runner with the NUnit framework to verify the core logic of independent modules, ensuring functional correctness and robustness. The following details the unit test content and coverage for cach module:

### Food Module Testing

Functionality Verification: In FoodTests.cs, test cases include:

## Page 12 of 46 — e During initialization, whether it correctly obtains the SpriteRenderer and

Source images: `IMG_20260414_160925.jpg`

Li éc: te : (=); e During initialization, whether it correctly obtains the SpriteRenderer and AudioSource components and configures audio properties (c.g., spatialBlend). e After calling RandomizePositionAndColor(), whether it generalcs a random position within the predefined grid area, correctly sets the food color, and updates the UI with the corresponding English word (matching the configured ColorWordPair). e Verifying whether GetCurrentColor() returns the current SpriteRenderer color as expected.

## Page 13 of 46 — NE ESSN SON ORAKOLOSLS CB, KEY Lont points include: ze

Source images: `IMG_20260414_160932.jpg`

NE ESSN SON ORAKOLOSLS CB, KEY Lont points include: ze e During initialization, whether tho snake corroctly: (=); ~ Creates a single hoad segment — Sets the default. movement direction to Vector2. right aa — IniWalizes the segments list properly e When moving via FixedUpdate(), whether: ~ The snake advances exactly 1 unit in the current direction = Subsequent segments follow the head correctly e When calling Grow(), whether: — A new segment is added to the snake body ~ The segment count increases accordingly e During collisions, whether: — Food-tagged objects trigger the Grow() method ~ Obstacletagged objects call ResetState(

## Page 14 of 46 — ee A SAU WOE VORLINE Whore iIndividtal unite or modules;

Source images: `IMG_20260414_160942.jpg`

ee A SAU WOE VORLINE Whore iIndividtal unite or modules; are ined and tested as a group to ovaluato tho intaractions between them, The “ts main pbjective is to ensure that different componcnts work together] correctly and oF that data flows scamlessly across modulo boundaries {1}. In our project, we conducted intogratiaa tests to verify the collaborative be- Casi haviour among key modules and ensure that the system functions as a unified whole. This type of testing is especially important in game development, where interactions between game logic, Ul clements, and player input must be consistent and bug-free. Below are the integration tests that were carried out to validate these interactions: : e Game Logic Integration: For example, whether Food. OnTriggerEnter2D calls ScoreManager to add points upon player collision while also triggering FakeFood.UpdateFakeFood to update colors.

## Page 15 of 46 — 2.2.3 System Testing a

Source images: `IMG_20260414_160951.jpg`

### 2.2.3 System Testing a

System testing focuses on validating the entire game os a complete and unified sys [s) tom. It ensures that all subsystems, including gameplay mechanics, user interface, — input handling, and visual/andio feodback, work together scamlessly to delivera smooth user expericnce [1]. In our project, we manually conducted full playthroughs of the Snake game to assess the end-to-end functionality and user interaction flow. These tests involved going through the complete cycle of starting the game from the welcome screen, navigating through the instruction and gameplay interfaces, interacting with ingame clements such as food and fake food, and observing the score updates and gamcover transitions. This testing confirmed that: e All UI transitions were triggered correctly at appropriate times. e Game logic and score tracking behaved consistently under various play conditions. e Visual and interactive feedback (¢.g., colour changes, object collisions, game a4 restart) matched user expectations.

## Page 16 of 46 — 2.3 ‘Testing Environment

Source images: `IMG_20260414_161006.jpg`

### 2.3 ‘Testing Environment

### The testing environment encompasses all technical infrastructure, tools, and conlig-

urations required Lo verify system functionality and validate implementation against : design specifications. This includes both the technical foundation for test execution and the methodological approach for test case development.

### 2.3.1 Unity Engine Version

e Primary development version: Unity 2022.3 LTS : e Specialized version for coverage analysis: Unity 6000.0 LTS (used for Code Coverage functionality) — 8 e Project developed in CH, utilizing Unity’s MonoBehaviour architecture e Testing infrastructure based on Unity Test Runner and NUnit framework (all test scripts implemented using Unity’s built-in testing tools)

## Page 17 of 46 — 2.4 Regression Strategy

Source images: `IMG_20260414_161036.jpg`

### 2.4 Regression Strategy

sos. b Rogression testing is a systematic approach to ensure Phat previously developed and tested software still performs correctly after modifications [1]. In the context of this Snake game project, we employ a multi-layered strategy combining smoke testing and regression testing to maintain software quality throughout the devclopment lifecycle.

### 2.4.1 Smoke Testing

The purpose of the smoke tests was to confirm that the basic features of the game were operational before proceeding to more comprehensive regression testing. Rapid ! vorification of basic functionality was conducted after cach build to check: e Whether the main scene can load successfully. e Whether critical buttons (Start, Pause, exit) respond correctly. , e Whether core interactions (snake cating, score updates) function without abwrinmenal ead renan

## Page 18 of 46 — Snake game project, we employ a multi-layered strategy combining smoke testing

Source images: `IMG_20260414_161049.jpg`

Sunesantat a teilliue Deareemtvei dient eel. fo: ee me BRED NATE AVUUULY GALUUL LULVULLLIVQUIVILLO {4}: aaa aos) eee Qh UALLS Snake game project, we employ a multi-layered strategy combining smoke testing and regression testing to maintain software quality throughout the development lifecycle.

### 2.4.1 Smoke Testing

The purpose of the smoke tests was to confirm that the basic features of the game were opcrational before proceeding to more comprehensive regression testing. Rapid verification of basic functionality was conducted after cach build to check: e Whether the main scene can load successfully. e Whether critical buttons (Start, Pause, Exit) respond correctly. e Whether core interactions (snake cating, score updates) function without abnormal returns.

### 2.4.2 Regeion Testing

To ensure that new features and bug fixes do not break existing functionality] we exccute the full test suite before cach major code commit or build. This process includes: : e Automated validation of all previously implemented core mechanics.

## Page 19 of 46 — WLLAVPULUL oY

Source images: `IMG_20260414_161057.jpg`

### WLLAVPULUL oY

### Test Plan

This chapter presents the test plan adopted in our project, detailing how testing was systematically integrated into the development process to ensure the reliability and correctness of the system.

### 3.1 Test Stages

The testing process was structured into four key stages: e Stage 1: Separate Unit Test: At this stage, team members performed unit testing on individual components using Unity Test Runner with the NUnit framework. The goal was to verify the correctness of each class and method

## Page 20 of 46 — e Stage 1: Separate Unit Test: At this Stage, team members performed unit

Source images: `IMG_20260414_161107.jpg`

e Stage 1: Separate Unit Test: At this Stage, team members performed unit testing on individual components using Unity Test Runner with the NUnit framework. The goal was to verify the correctness of each class and method in isolation, focusing on core functionality such as game logic, UI states, and scoring mechanisms. e Stage 2: Integration Test: After unit testing, we tested the interactions between components to censure modules worked correctly when combined. This included verifying that player actions triggered the appropriate updates in the UI, game logic, and score manager. e Stage 3: System Test: We conducted full playthroughs of the Snake game Lo validate the entire game flow from the Rser’s perspective. This involved checking gameplay, input handling, UI transitions, and edge cases such as collision resets and pause/resume functionality. e Stage 4: Fix Bugs: Throughout all testing stages, bugs were logged and tracked by team members. Issues discovered during unit, integration, or syslem testing were addressed iteratively through debugging and patching until the desired behaviour was achieved. a9 Tract Taclea A llanatian ates

## Page 21 of 46 — 3.3 Full Test Plan

Source images: `IMG_20260414_161129.jpg`

### 3.3 Full Test Plan

The test plan was documented and published in our GitLab repository for version control and collaborative access, which is accessible at: Test. Plan (Part of Weekly lion”, “to do”, "ready to merge”, cle. According to the needs and process of development, she also set up three milestones. Kai opened the Unittest issue and assigned it to the corresponding part of the team. Runhan opened the Integration issuc and collaborated with Kai to complete the Integration tests, while Longyu, Qi- ) uyang and Yaqiao completed assigned tests. Meanwhile, our team strictly followed the requirement of testing in the new branch during the testing process. After the test passed, Lcam member requested a branch merge, which was checked by another team member. Such approach guarantecs that no development related work will be done on the Main branch. A copy of the finalized test plan has been extracted and included as a PDF in Appendix B of this report for reference.

## Page 22 of 46 — Validation Results

Source images: `IMG_20260414_161137.jpg, IMG_20260414_161145.jpg`

° °

### Validation Results

### 4.1 Current Test Coverage

R Tost. coverage is an important measure of the completeness of software testing, reflecting the percentage of code that has been executed by test cases. High test coverage Is often a positive indicator of code reliability, though it should be complemented by meaningful and well-designed test cases. The current test coverage is analysed in detail from two dimensions, Code Coverage Metrics (Figure 4.1) and Requirements Coverage Matrix (Table 4.1), respectively. Firstly, the Code Coverage Metrics provide a breakdown of test coverage for cach module in the project, including both line coverage and method coverage. Sccondly, the Requirements Coverage Matrix maps individual test cases to specific functional requirements, verifying whether all project requirements have been sufficiently tested through corresponding test scenarios.

### 4.1.1 Code Coverage Metrics

4 e Coverable lines: 981

## Page 23 of 46 — i © Name v Covered Uncovered ~Coverable » Total v Line coverage

Source images: `IMG_20260414_161200.jpg`

i © Name v Covered Uncovered ~Coverable » Total v Line coverage = MalnAssembly $09 64 973 s« 7984 62.6% GEE FakoF ood $4 13 67 130 60.5% Se Food 52 11 63 138 62.5% SE Instruction 25 0 25 47 100%. SEE ; PausoManager 47 19 68 152 712% SS Pauso_ExiManagor 13 13 26 58 SO% GE _ScoroManagor 25 0 25 '§2 100% SE Snako 43 8 51 73 04.3% ST StariMenuManager 25 0 25 48 100% SED WelcomeMonu 3 0 2 al 100% Si = Tests 638 10 608 =: 1439 00.3% GHEE FakeFoodTests 70. 3, 82 170 06.3% SEE : FoodTests 118 3 116 219 07.4% Ss _Gameointograhon Tests 78 0" 7S 154 100% CEE nskuctionMonu Tests 20 ‘0 20 56 100% SE Nonutntegraton Tes! 31 0 ) 65 100% Sana

### PauseExlManagorTests 37 4 49 108 OO 2t, SURES

Pausedtanagerintegration Tes! 21 0 vt $4 100% aE PauteMariagerTosts 59 ‘O 80 139 1OO% Gl _ScoreManagorTosts ay 0 al 102 ‘100% SER SuakeTosts 73 0 73 159 ‘100%, GLEE StartitenuManagerTosts 20 0 2 56 100%, SUED _YestPauspE miAlanagor 29 0 “9 108 100% Santen WelcomoManu Tests 20 0 20 6 100% N Figure 4.1: Line Coverage by Assembly a Req. Description Test Case(s) Status

## Page 24 of 46 — _v Name ~ Covered wUncovered -~ Coverable ~ Total v Line coverage

Source images: `IMG_20260414_161203.jpg`

_v Name ~ Covered wUncovered -~ Coverable ~ Total v Line coverage = MalnAesembly 3 $09 64 378 —s«734 02.6% foo FakoFood $4 13. 67 130 605% Food 52 11 63 136 625% a Instruction 25 0 25 47 100°, EEE PauseManager 47 19 66 152 1.2% Ge Pauso_ExiManagor 13 13 26 58 50%. ScoreManager 25 o 25 52. 1002, moe _Snako 43 8 51 73. 843%, C= StartMenuManager 25 0 25 45 100% Ey

### WelcomeMeonu 25 0 25 41 1002, EI

= Tests a) 10 608 1439, 93.3% (oo FakeFoodTests 79 3) 82 170 96.3% Ss FoodTests: 113. 3 116 219: 97.4%, EE Gamelntegration Tests 75 0 75 154 100%

### InstructionMenuTests 20, 0 20 56 1002, Gh

MonulntegrationTest 31 0 31. 65 100°, Sie PausoExitManagerTests 37 4 41 108 90.23> EE PauseManagorintegrationTest 21, 0 21 54 100%> EE PauseManagorTests 59 0 59 132 100%> EEE ScoreManagerTests 4. 0 41 102 100°; Qa SnakeTests 73 0 7 = 188 100: REE StartMenuManagoerTests 20. 0 20 56 100°, GEES TostPauseExiManager 9 0 9 108 100:, SEE WelcomeMenuTests 20 0. 20 56 100% RRS I‘igure 4.1: Line Coverage by Assembly y Reg. Description Test Case(s) Status rere

## Page 25 of 46 — RL. ood processing logie PakeFoodTests, Covered]

Source images: `IMG_20260414_161207.jpg`

RL. ood processing logie PakeFoodTests, Covered] oodTests (80%) R-2 Instruction menn = InstructionMenuTests. = Covered Ninetionality (100%). RY Pause & Exit manager Pausel>xitManagerTests, Pa rbinity functionality Test Pauselexit Manager covered R-4 Pause manager ~ PauscManagerTests = Partially functionality covered a 2%)

### It-5 Score manager ScoreManager'Tests Covered

functionality (100%) R-6 — Shake gameplay ~ SnakeTests Covered mechanics (100%)

### R-7 Start menu navigation Start MenuManagerTests Covered

ee 10%)

### RS WelcomeMenuTests Covered

### IX-9 Overall game integration GamelntegrationTests, Covered

scenarios MenulntegrationTest, (100%)

### PauseManagerIntcgra-

### LionTest

## Page 26 of 46 — Figure 4.1: Line Coverage by Assembly

Source images: `IMG_20260414_161210.jpg`

TosiPausoExtManager 9 0 9 108 100% GE WolcomoMenutTosis 20 0 20 56 100% Sua Figure 4.1: Line Coverage by Assembly 1D = ID R-1 lood processing logic Fakcl*oodTests, Covered RNS [Raita 09

### R-2 Instruction menu InstructionMenuTests Covered

“oo

### R-3 Pause & Exit manager PausclxitManagerTests, Partially

functionality covered

### R-4 Pause manager PauscManagerTests Partially

functionality covered

### R-5 Score manager ScoreManager Tests Covered

ftwectonnty [ C0

### R-6 Snake gameplay SnakeTests Covered

[nodes fm

### R-7 Start menu navigation StartMcnuManagerTests Covered

(1nn%)\

### Detected Tables

| Description -_ ae | Test Case(s) ai |
| --- | --- |
| Food processing logic a | akcFoodTests, ood Tests |
| Instruction menu functionality | InstructionMenuTests R |
| Pause & Exit manager functionality | PauscExitManagerTests, — TestPausclxitManager |
| Pause manager functionality | PauscManagerTests |
| Score manager functionality | ScoreManagerTests |
| Snake gameplay -mechanics | SnakeTests |


## Page 27 of 46 — e Defects found: 5

Source images: `IMG_20260414_161216.jpg`

e Execution environment: Unity 2022.3.0f1, Test Framework 1.1.2, Report- Goncrator 5.0.4, Windows 11 Pro, 32GB RAM, Intel Core i9-1390011X e Run duration: 0.361 (full suite) e Avorage per test: ~0.01s e Defects found: 5 — Critical: 2 (game start failure) — Major: 3 (pause logic Soundary, score rollback) — Status: 5 fixed The current test coverage data shows that the overall code line coverage of the project reaches 92.4% and method coverage reaches 95.8%, indicating that the core functionality has been fully verified. However, some modules (c.g., Pause_ExitManager and PauseManager) have lower coverage (50%-71.2%), which may necd to be prioritised for additional testing in the future to avoid hidden defects affecting user expericnee. And in this test, a total of 36 test cases were executed, of which 31 passed, resulting in a pass rate of 86.11%. There were 5 failed cases (13.89%), all of which involved critical functionality tests (c.g., game start and pause logic boundary issucs). The total running time for the entire test suite was 0.361 seconds, with an average time of approximately 0.01 seconds per test case. All 5 bugs found (2

## Page 28 of 46 — an average time of approximately 0.01 seconds per test case. All 5 bugs found (2

Source images: `IMG_20260414_161219.jpg`

an average time of approximately 0.01 seconds per test case. All 5 bugs found (2 Critical, 3 Major) have been fixed, including: gaine startup failures (Critical) and pause logic boundary and score rollback issucs (Major).

### 4.2 Defect Analysis

### 4.2.1 Defect Analysis

### Overview

This testing process covers three types of testing: functional test, intcgration test, and system test. The functional tests include the following modules:

## Page 29 of 46 — e Fakclood Module Testing

Source images: `IMG_20260414_161222.jpg`

e Fakclood Module Testing e ScoreManager Module Testing e Pause and Pause_Exit Module Testing e¢ Menu Module Testing The integration tests include three aspects: e Game aie Integration e UI Interaction The system test simulates real scenarios to check if the overall workflow is coherent.

### Defect Statistics and Distribution

The following scctions summarise the number of defects identified during different testing stages, as well as their severity levels. e Unit Test: 4 e Integration Test: 1

## Page 30 of 46 — Defect Type and Cause Analysis

Source images: `IMG_20260414_161230.jpg`

R

### Defect Type and Cause Analysis

The following lists the five defects found during testing with their details: l. ‘Test Name: TestStart_W ith Valid Dependencies_CallsRandomizePositionAndColor Class: Food Type: Functional defects (failed color validation) Cause: The colorWordPairs sct contained colors beyond red and green, or initialization didn’t cnsure only these two colors were present.

2. Test Name: TestRandomizcPositionAndColor_UpdatesPositionColorAndText

Class: Food Type: Functional defects (failed color validation) Cause: Same as defect (1), the color options didn’t mect the red or green” requirement.

## Page 31 of 46 — Cause: Scene wasn’t properly added to Build Settings.

Source images: `IMG_20260414_161234.jpg`

Cause: Scene wasn’t properly added to Build Settings.

### Improvement Measures and Suggestions

e Process Improvements: — Strengthen requirements communicatio® and definition — Improve code review processes e Technical Improvements: — Enhance color check logic in colorWordPairs collection — Add validity checks for Snake segment collection — Explicitly implement score reset logic in ResetScore method e Preventive Measures: — Implement Continuous Integration and Testing — Establish a defect knowledge base

## Page 32 of 46 — Summary and Reflections

Source images: `IMG_20260414_161244.jpg`

a I a

### Summary and Reflections

### 0.1 Team Members and the Division of Labor

This section provides a description of the team members, their roles and responsibilities during the software testing phase and exccution of testing plan. Sev- ; cral adjustments were implemented in team roles and the division of labor during ) the software testing phase. While the majority of the initially assigned roles remained unchanged, additional roles and responsibilities were allocated to certain team members to ensure a fair distribution of tasks and a balanced workload. These modifications were made to enhance efficiency and maintain alignment with agile development.

### Name Role

### LI Qiuyang Technical Writer, Tester, Rescarcher

### LIU Runhan Technical Lead, Technical Writer, Tester

### Woaroen-K nyveazvevn

## Page 33 of 46 — The v2.0 build is stable, with refined gameplay and UI, sotting the stage for

Source images: `IMG_20260414_161248.jpg`

NEE EEEENEEIENENLA ENE NRNAUNFARACTUULALR Wiad UAba gy WAU Waa ’ JCRACEAAN LAA, Cpeiituy (haere quality. The v2.0 build is stable, with refined gameplay and UI, sotting the stage for the final v3.0 deliverablo. A Kanban board is being used to track task progress, hosted on the shared spreadsheet alongside other project documentation, Figure 5.1 below is a preview of our Kanban as of April 11, 2025. : = (7 palma meramunsi oon [oon ar rn eee 3 a ome Reece eee el eel Nee Weekly Bape: Dosewes usin bes aie br Aeen irc: ee ees ORME SOs eben ene Ona } 7t Wao, 2399 — a ee _rtt“‘<i‘R —p ie bees cal = %—T Lote al downer anon tet caren Se a. SS ta pereee_ eos ——_____ cic Oe jt es Bee = Gece weet tno ise SS ae — —____lweganereenm ______beeste ncn fen Gre tt nmap Met N wermem wo (_ Siena cies Reyraen Yaegy, l as ———$ Pe Reread a — a Ss Druonte B Gocomentanan parm Testy Ctynctore,; at __ 4 eee stant tenes tent Lmenw rere - RRS ae Team Vraden ext i bursary dommsnonenmn JOA Cremen Vor Merry ac ————— rt 88 Ot oc; ee i ‘ ae ee ee ee ee M2 Age 172) te ee = Roles&Respoasidilities Project plan (Kanban) +

### Detected Tables

| rr, 7 m= ren ©: he testing pre i 2 needs ered tart ~ Z; t oe om 7, oe Tor fh ’ Pe pee for de ~~ .: ary ry i } sano; no a a Ss eteesnannnsssensetits (Bp. “susastamnnteneansen ‘Vaan (eo tks Deteren: trae R ileal ener | Column 2 | Tae RI GANS. © + Meee Lh oe SETA Uwe os ot RS Ba Po I ee ee | Column 4 | eae rahe | , ot 5, a ae: ies! Raat ta SS ber vags aoe | - Rie soe < eS coy se ang — ee sa x a e i Ce ae on) ‘en > g ek |
| --- | --- | --- | --- | --- | --- | --- |
|  |  | TT |  |  |  |  |
| Mhoeey Ceo ang BAI RD poco cine ec ll Rte |  |  |  |  |  |  |
|  | ‘wl ta a Pees FOR i EL Lae ant att eee Awe cows ayn SheerVerage |  |  |  |  |  |
|  |  |  | mnt | * z - a 7 |  |  |


## Page 34 of 46 — Milestone Deadline

Source images: `IMG_20260414_161252.jpg`

### Milestone Deadline

Weekly-repert—l—Requirement-Speeification 45th-ofFMareh-2025,-5-p-nay Team Project Website 21st of April 2025, 5 p.m. Iinal Software Submission 21st of April 2025, 5 p.m. Final Group Report 21st of April 2025, 5 p.m. Demonstration Vidco 21st of April 2025, 5 p.m. Presentation 22nd of April 2025, 5 p.m. Table 5.2: Mileston®s and Deadlines For the upcoming stage, our team will focus on finalising the project, including refining minor [catures to ensure full alignment with the project requirements. Following this, we will complete the final group report and begin preparing the demonstration vidco as well as the release presentation.

## Page 35 of 46 — IMG_20260414_161255

Source images: `IMG_20260414_161255.jpg`

(4) Hays. Technical writer - jobs, tasks & salary, 2025. Accessed: 2025-04-10. ly

## Page 36 of 46 — Unit Test

Source images: `IMG_20260414_161308.jpg`

### Unit Test

® Class: Snake e Class: Food e Class: FakeFood e Class: WelcomeMenu ° Class: Instruction e Class: StartMenuManager e Class: Pause ExitManager’ e Class: ScoreManager e Class: PauseManager Class: Food Test: TestAwake_InitializesComponents

## Page 37 of 46 — Test Inputs Expected Outcome Test

Source images: `IMG_20260414_161345.jpg`

### Test Inputs Expected Outcome Test

### Outcome, Result Remark

Food's color should be either red or green: Pass 1 N/A true Test: TestRandomizePos itionAndColor UpdatesPositionColorAndText d — Result Remark

### Test Inputs Expected Outcome eiicennie

### Food's updated color should be either red or Fail

oe 1 N/A false

## Page 38 of 46 — 1 N/A initial} nie gee ee SpriteRenderer and AudLosource are

Source images: `IMG_20260414_161351.jpg`

1 N/A initial} nie gee ee SpriteRenderer and AudLosource are ae and AudioSource. spatialBlend set to correctly initialized and spatialblend is mass Test: TestStar *_WithValidDependencies_CallsRandomizePositionAndColor

### Test Inputs Expected Outcome Test

### Outcome Result Remark

1 N/A Food's color should be either red or green: ealas Fall Ly public void RandomizePositionAndColor() // Pick a random color-word combination int index = Random.Range(0, colorWordPairs.Count); ColorWordPair selectedPair = colorWordPairs [index]; . // Set the color of the sprite renderer to the selected color SpriteRenderer.color = selectedPair.color;

## Page 39 of 46 — EEE ELEESDSESR “OO LNO SOLOECted color

Source images: `IMG_20260414_161406.jpg`

EEE ELEESDSESR “OO LNO SOLOECted color SpriteRenderer.color = selectedPair.color; if (colorWordText = null) colorWordText.text = selectedPair.word; R

### Test k

### Result Remar

### Test Inputs Expected Outcome Outcome

,. Pass e Food's color should be either red or green: an pmnainvinnae os 1 N/A

## Page 40 of 46 — Test Inputs Expected Outcome

Source images: `IMG_20260414_161429.jpg`

### Test

### Test Inputs Expected Outcome

p p Giitesme Result Remark i Food's color should be either red or green: Pass 1 N/A true Test: TestRandomizePositionAndColor UpdatesPositionColorAndText

### Test Inputs E ted Out on Result Remark

es npu xpected Outcome outcome , NA Food's updated color should be either red or pam Fail aise green: true @04/02/2025

## Page 41 of 46 — Bice, true WUdIUEIZUZ9

Source images: `IMG_20260414_161441.jpg`

### Bice, true WUdIUEIZUZ9

public void RandomizePositionAndColor() // Pick a random color-word combination int index = Random.Range(0, colorWordPairs.Count); ColorWordPair selectedPair = colorWordPairs[index); I // Set the color of the sprite renderer to the selected color spriteRenderer.color = selectedPair.color; if (colorWordText!= null) colorWordText.text = selectedPair.word;

### E d Out ue Result Remark

### Test inputs xpected Outcome Siconie

### Food's updated color should be either red or Pass

4 ALA trie

## Page 42 of 46 — Test: OnTriggerEnter2D NonPlayerCollision_DoesNothing

Source images: `IMG_20260414_161449.jpg`

Capuicy. Test: OnTriggerEnter2D NonPlayerCollision_DoesNothing Test Inputs Expected Outcome TescCOutcome! Result Remark Fiteed fi stayed inactive, 1 When anon plyer collides with = hg erepypane! haketesd, no changes should phage jnactive, ona 1 fakePood.OnTriggerEnter2D(nonPlayerCollider) occur (error panel should stay An Kere @04/02/2025 inactive, score should remain remained fy the same). unchanged. Test: TestColorsEqual_Method

## Page 43 of 46 — Initially, segment count should be 1. After Segrnent count is ps Segment

Source images: `IMG_20260414_161453.jpg`

Initially, segment count should be 1. After Segrnent count is ps Segment SS LalliNg, car, segment count Id be ially,. 7 a ount twWwA FOMING Ha TG). SEB should he, initially, (agi2 after (04/02/2025 b prien

2. growing. veritied

} ‘ Test: Snake_Grow_AddsSegment i : Test Inputs Expected Outcome Test Outcome Result Remark in The snake shocelpiow by fe6iment count increases by Pass BPowth expoent. after arevinGe @04192/2025 —ywesifigg, Test: Snake_Grow_AddsSegment_AtCorrectPosition b 4 f The mew $ UG, 0,

## Page 44 of 46 — The new segment should be positioned at the previous (5, 5, Pass ee

Source images: `IMG_20260414_161503.jpg`

The new segment should be positioned at the previous (5, 5, Pass ee me tail's position ((5, 5, 0) ) 04/02/2025 oo P (29, aE ” @ positioning Test: TestResetState_ClearsSegmentsAndResetsPosition

### Test Inputs Expected Outcome Test Outcome Result Remark

After resetstate(), only the snake head should Only 1 segment remains State as Pass 1 N/A remain and its position should reset to (0, 0, at (0, 0, 9) after reset 0). reset. verified b Test: TestOnTriggerEnter2D Food _CallsGrow : Test Inputs Expected Outcome Test Outcome Result Remark -,, Food ' NIA After colliding with food, the snake Segment count increases by 1 Pass colileion should grow by 1 segment. after collision with food. @04/02/2025 verified . Test: TestOnTriggerEnter2D Obstcle_ CallsResetState

## Page 45 of 46 — Class: Instruction

Source images: `IMG_20260414_161512.jpg`

Class: Instruction Test: TestInitialState

### Test Inputs Expected Outcome Test Outcome Result Remark

onditi:

### The startMenuPane) should be active (visible) vctaphecalcaceghachaben ses

otartNenuPanel.activesSelf raturns Pass \ N/A when the game first starts and the gameParent FT tree @04/05/2025 is inactive (hidden) when the game first starts. oe 1 OCCAVOES 102 returns false Test: TestStartButtonClick

### Test inputs Expected Outcome Test Outcome Result Remark

When the Stan Button is clicked, then the Both conditions are true: !, NA atartNenuPanel should become inactive otartHenuPanel.activeSelf is false (menu Pass (hidden) and the gameParent should become _ is hidden) and gameParent.activeSelf is @04/05/2025 active (visible). true (game Ul is visible). Class: StartMenuManager Test: TestInitialState z Test inputs Expected Outcome Test Outcome Result Remark

## Page 46 of 46 — 1 N/A Instruction component exists: true. True, EaSS SUE EMOUICCEPOReOt

Source images: `IMG_20260414_161519.jpg`

1 N/A Instruction component exists: true. True, EaSS SUE EMOUICCEPOReOt @04/08/2025 verified After clicking, Instruction should be hidden: Pass 2 N/A True. rifi wen eciiosies Instruction verified After clicking, gameParent should be active: Pass 3 N/A True. rent verifi rue. rue »@04/08/2025 gameParent verified ly Test: TeststartMenuPanel Test Inputs Expected Outcome = Result Remark

### P Outcome

### Pass StartMenu component

A::;; 1 N/ StanMenu's star(MenuPanel should be active: True True 2025 verified Sh ae anes Pass StartMenu’s gameParent 2 N/A StartMenu'’s gameParent should be inactive: True. True. 025 verified After clicking, StartMenu's startMenuPanel should be Pass; N/A True. startMenuPanel verified 3 hidden: True. @04/08/2025 After clicking. gameParent in StartMenu should be active: Pass True. meParent verified aaa ee trae’ me @owos2025 = mmr orenswen _ Class: PauseManagerScoreIntegrationTest
