const resetLoading = "RESET_LOADING";
const requestSurveys = "REQUEST_SURVEYS";
const receiveSurveys = "RECEIVE_SURVEYS";
const requestSurvey = "REQUEST_SURVEY";
const receiveSurvey = "RECEIVE_SURVEY";
const requestSurveyQuestionMutation = "REQUEST_SURVEY_QUESTION_MUTATION";
const receiveSurveyQuestionMutation = "RECEIVE_SURVEY_QUESTION_MUTATION";
const graphQlService = "graphql";
const initialState = { surveys: [], isLoading: true, survey: null };
export const actionCreators = {
    resetLoading: () => function (dispatch, getState) {
        dispatch({ type: resetLoading })
    },
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
    },
    requestSurveyQuestionMutation: (questionId, surveyId, name) => async function (dispatch, getState) {
        dispatch({ type: requestSurveyQuestionMutation });

        const response = await fetch(graphQlService, {
            method: "POST",
            headers: new Headers({
                "accept": "application/json",
                "content-type": "application/json"
            }),
            body: JSON.stringify({
                query: `mutation($question: QuestionInput!, $questionId: Int!) {
                    updateQuestion(question: $question, questionId: $questionId) {
                        name
                    }
                }`,
                variables: { "question": { "name": name, "surveyId": surveyId }, "questionId": questionId }
            })
        });
        var isOk = response.status == 200;
        dispatch({ type: receiveSurveyQuestionMutation, isResolved: isOk });
    }
};

export const reducer = function(state, action) {
    state = state || initialState;
    switch (action.type) {
        case resetLoading:
            return {
                ...state,
                isLoading: true
            };
        case requestSurveys:
            return {
                ...state,
                isLoading: true
            };
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