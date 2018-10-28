const requestSurveys = "REQUEST_SURVEYS";
const receiveSurveys = "RECEIVE_SURVEYS";
const requestSurvey = "REQUEST_SURVEY";
const receiveSurvey = "RECEIVE_SURVEY";
const graphQlService = "graphql";
const initialState = { surveys: [], isLoading: false, survey: null };
export const actionCreators = {
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
    },
    requestSurvey: id => async function (dispatch, getState) {
        dispatch({ type: requestSurvey });

        const response = await fetch(graphQlService, {
            method: "POST",
            headers: new Headers({
                "accept": "application/json",
                "content-type": "application/json"
            }),
            body: JSON.stringify({
                query: `query($id: Int!) {
                          survey(id: $id) {
                            id,
                            name,
                            questions {
                              id,
                              name
                            }
                          }
                        }`,
                variables: { "id": id }
            })
        });
        const responseAsJson = await response.json();
        dispatch({ type: receiveSurvey, survey: responseAsJson.data.survey });
    }
};

export const reducer = function(state, action) {
    state = state || initialState;
    switch (action.type) {
        case requestSurveys:
        case requestSurvey:
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
        case receiveSurvey:
            return {
                ...state,
                isLoading: false,
                survey: action.survey
            };
        default:
            return state;
    }
}