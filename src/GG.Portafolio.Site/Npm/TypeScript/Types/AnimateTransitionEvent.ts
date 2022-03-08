import { AnimateTransitionEventType } from './../General/Enum/AnimateTransitionEventType';

 export type AnimateTransitionEvent<T> = {
    EventType: AnimateTransitionEventType,
    Event(pagechange: T, event: AnimationEvent): void,
    ParamEvent: T
}