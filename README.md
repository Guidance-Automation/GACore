 ____    ___  _____                
|  __ \ / _ \/  __ \               
| |  \// /_\ \ /  \/ ___  _ __ ___ 
| | __ |  _  | |    / _ \| '__/ _ \
| |_\ \| | | | \__/\ (_) | | |  __/
 \____/\_| |_/\____/\___/|_|  \___|
                                  
https://github.com/GuidanceAutomation/GACore
https://www.guidanceautomation.com/

v2.5.2 (16th Mar 21)

* Adds No Scanner Data brush dictionary value.

v2.5.1 (23rd Feb 21)

* Style sheet updates

v2.5.0 (18th Feb 21)

* Adds another legacy control, the IPAddressControl which again is from GAWPFCore and no-longer supported. 

v2.4.0 (18th Feb 21)

* Adds the (largely defunct) IsTrueCircleControl, which is legacy in GAWPFCore package and no-longer supported. 

v2.3.2 (29th Jan 21)

* Allows derived classes to trigger the handle collection refresh for AbstractCollectionViewModel. 

v2.3.1 (15th Jan 21)

* Add ToFleetManagementMetadataDto() extension for FleetManagementMetaData

v2.3.0 (12th Jan 21)

* Adds functionality to get FleetManagementMetadata from the registry.
* Adds generic DialogShutdownMessage

v2.2.2 (6th Dec 20)

* Remove duplicate IsInFault() ex method (now in GAAPICommon).

v2.2.1 (6th Dec 20)

* Fixes binding error on KingpinStateView

v2.2.0 (13th Nov 20)

* Adds IVersionable

v2.1.0 (13th Nov 20)

* Add IRestartRequestable
* Sorts out project solution folders

v2.0.1 (12th Oct 20)

* PiWrap bugfix for feeding in large numbers, I.e. 1e300.

v2.0.0 (28th Aug 20)

* Strips out the IKingpinState and its dto (formerly KingpinStateData) to the GAAPICommon assembly as this .net standard and can be integrated into .net core components.

v1.25.7 (5th Aug 20)

* Adds Exception to IResult and IResult<T>

v1.25.6 (1st July 20)

* No functional changes, switches to hardcoding Xceed 3.8.2 to get round license issue for the moment. 

v1.25.5 (30th June 20)

* Moves dtos out to GAAPICommon using NetStandard 2.0

v1.25.4 (30th June 20)

* Unifies sem ver implementations
* Adds common dtos
* Adds exception ability to results factory

v1.25.3 (22nd June 20)

* Adds AgentLifetimeState enumerator
* Improves result factory

v1.25.2 (6th April 20)

* Fixes embarrassing IGenericResult<T>

v1.25.1 (31st Mar 20)

* AbstractCompositionTargetControl shouldn't be abstract :facepalm:

v1.25.0 (31st Mar 20)

* Adds AbstractCompositionTargetControl

v1.24.4 (30th Mar 20)

* Switchable invoke behavior
* View bugfixing.

v1.24.3 (30th Mar 20)

* Trace logs INotifyPropertyChanged for AbstractViewModel

v1.24.2 (30th Mar 20)

* Makes the invoke, BeginInvoke on AbstractViewModel. Returns control instantly. 

v1.24.1 (23rd Mar 20)

* Quantization bug fix and associated unit test

v1.24.0 (23rd Mar 20)

* Adds IKeyedEnumerable
* Adds Point quantize methods. 

v1.23.0 (10th Mar 20)

* Add extension methods for Size (subtract, scale & max)

v1.22.2 (10th Mar 20)

* Adds parameterless constructors for generics for xaml support

v1.22.1 (28th Feb 20)

* Minor logging updates to AbstractViewModel<T>

v1.22.0 (19th Feb 20)

* Minor UI tweeks
* Makes the dummy kingpin tool not use INotifyPropertyChanged
* Cleans up generic mailbox


v1.21.0 (9th Jan 20)

* Adds Button Control
* Adds Checkbox Button Control

v1.20.2 (16th Dec 19)

* Improves styling of some UI elements. 

v1.20.1 (3rd Dec 19)

* IPAddressView default value bugfix.

v1.20.0 (2nd Dec 19)

* Adds GACore.UI assembly.
* Adds IPAddressView for MVVM.

v1.19.4 (28th Nov 19)

* Adds missing OnNotifyPropertyChanged to AbstractViewModel.

v1.19.3 (26th Nov 19)

* AbstractCollectionViewModel bugfix.

v1.19.2 (26th Nov 19)

* AbstractCollectionViewModel bugfix.

v1.19.1 (26th Nov 19)

* ViewModel HandleModelUpdate() now returns old and new values to faciltate better event handling.

v1.19.0 (21st Nov 19)

* Adds GetTempFilenameWithExtension to tools

v1.18.0 (20th Nov 19)

* Adds Abstract Collection View Model for self maintaining observable collection of view models.

v1.17.2 (12th Nov 19)

* Extended WPF Toolkit (http://schemas.xceed.com/wpf/xaml/toolkit) style sheet

v1.17.1 (8th Nov 19)

* View Bugfix

v1.17.0 (6th Nov 19)

* Adds CustomCommand object for MVVM

v1.16.0 (6th Nov 19)

* Adds messenger utility for inter-view model signalling. 
* Adds logger for view models

v1.15.0 (29th Oct 19)

* Switches to MVVM pattern for KingpinState and KingpinStatus views. 
* Allows views to update at FPS rate, rather than kingpin state updates which may occur at high frequency and flood dispatcher thread. 

v1.14.1 (29th Oct 19)

* Adds boilerplate MVVM components

v1.14.0 (14th Oct 19)

* Adds enumerators for Task type, Status, etc. 

v1.13.1 (8th Oct 19)

* Fixes missing abs check to AreWithinRadTol.

v1.13.0 (8th Oct 19)

* Adds AreWithinRadTol to Maths. 

v1.12.2 (26th Sep 19)

* .net472 framework 
* Removes duplicate extension methods between GACore.ExtensionMethods (class) and GACore.ExtensionMethods (assemblies).

v1.12.1 (26th Sep 19)

* Adds GACore.Extensions	
** Useful extension methods (50+)
** Mainly for manipulating collections
* Adds GACore.Generics
** Containers for comparative objects
* Adds Tignometry static tools
* Adds Maths static tools 
** LinSpace for creating linearly spaced arrays

v1.11.1 (25th Sep 19)

* Adds missing methods on ResultFactory

v1.11.0 (25th Sep 19)

* Adds ResultFactory

v1.10.0 (23rd Sep 19)

* Adds IResult and IGenericResult<T> interfaces and implementations. 

v1.9.0 (18th Sep 19)

* Adds NLogManager and associated components.

v1.8.2 (16th Sep 19)

* Removes poorly thought-out value constraint on key for GenericMailbox.

v1.8.1 (16th Sep 19)

* Fixes GenericMailbox failed branch merge.

v1.8.0 (16th Sep 19)

* Add GenericMailbox

v1.7.0 (9th Sep 19)

* Adds IPV4 control

v1.6.0 (20th Aug 19)

* Adds KingpinStateControl & children
* Adds IsConnectedControl
* Completes abstraction of kingpin state to a single package.

v1.5.0 (16th Aug 19)

* Adds KingpinFaultSatus code to diagnoise IKingpinStates for faults.

v1.4.0 (16th Aug 19)

* Adds MovementType enumerators
* Extends IKingpinState to include movement type. 

v1.3.2 (14th Aug 19)

* Fixes KingpinStateData not implementing IKingpinState

v1.3.1 (14th Aug 19)

* Adds GACore.Architecture assembly
* Moves some enums defs to GACore => GACore.Architecture
* Adds interface definition for IKingpinState

v1.3.0 (14th Aug 19)

* Adds KingpinStateData object.
* Adds enum definitions for frozen and auto control.
* Adds extension method for tick comparison.

v1.2.0 (13th Aug 19)

* Adds SemVerData object.

v1.1.0 (12th Aug 19)

* Adds Banner Q45 control.

v1.0.0 (6th Aug 19)

- Initial release
* Kingpin state enumerators
* Kingpin status control
