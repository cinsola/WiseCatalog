import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerMiddleware, connectRouter } from 'connected-react-router';
import * as SurveysStore from './SurveysStore';
import * as SurveyStore from './SurveyStore';
export default function configureStore(history, initialState) {
    const reducers = {
        router: connectRouter(history),
        surveysReducer: SurveysStore.surveysReducer,
        surveyReducer: SurveyStore.surveyReducer
    };


    const middleware = [
        thunk,
        routerMiddleware(history)
    ];

    // In development, use the browser's Redux dev tools extension if installed
    const enhancers = [];
    const isDevelopment = process.env.NODE_ENV === 'development';
    if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
        enhancers.push(window.devToolsExtension());
    }

    const rootReducer = combineReducers({
        ...reducers
    });

    return createStore(
        rootReducer,
        initialState,
        compose(applyMiddleware(...middleware), ...enhancers)
    );
}
