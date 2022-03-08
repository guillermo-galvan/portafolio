import { AnimateTransitionEvent } from './../Types/AnimateTransitionEvent';

export type AnimateTransitionType<T> = {
    IdElement: string,
    Events: AnimateTransitionEvent<T>[] | undefined,
};