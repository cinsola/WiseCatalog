const requestSurveys = "REQUEST_SURVEYS";
const receiveSurveys = "RECEIVE_SURVEYS";
const graphQlService = "graphql";
const initialState = { surveys: [], isLoading: true };
export const surveysActionCreators = {
    requestSurveys: () => async function (dispatch, getState) {
        dispatch({ type: requestSurveys });
        const response = await fetch(graphQlService, {
            method: "POST",
            headers: new Headers({
                "accept": "application/json",
                "content-type": "application/json"
            }),
            body: JSON.stringify({
                query: `{
                  surveys {
                    id
                    name
                  }
                }`
            })
        });
        const responseAsJson = await response.json();
        dispatch({ type: receiveSurveys, surveys: responseAsJson.data.surveys });
    }
};

export const surveysReducer = function (state, action) {
    state = state || initialState;
    switch (action.type) {
        case requestSurveys:
            return {
                ...state,
                isLoading: true
            };
        case receiveSurveys:
            return {
                ...state,
                isLoading: false,
                surveys: action.surveys
            };
        default:
            return state;
    }
};