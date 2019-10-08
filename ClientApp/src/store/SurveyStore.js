const requestSurvey = "REQUEST_SURVEY";
const receiveSurvey = "RECEIVE_SURVEY";
const requestSurveyQuestionMutation = "REQUEST_SURVEY_QUESTION_MUTATION";
const receiveSurveyQuestionMutation = "RECEIVE_SURVEY_QUESTION_MUTATION";

const graphQlService = "graphql";
const initialState = { isLoading: true, survey: null };
export const surveyActionCreators = {
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
    requestSurveyQuestionMutation: (surveyId, questionId, name) => async function (dispatch, getState) {
        dispatch({ type: requestSurveyQuestionMutation, questionId: questionId });

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

        dispatch({ type: receiveSurveyQuestionMutation, questionId: questionId, question: (await response.json()).data.updateQuestion });
    }
};

export const surveyReducer = function (state, action) {
    state = state || initialState;
    switch (action.type) {
        case requestSurvey:
            state.isLoading = true;
            return {
                ...state,
                isLoading: true
            };
        case receiveSurvey:
            return {
                ...state,
                isLoading: false,
                survey: action.survey
            };
        case requestSurveyQuestionMutation:
            return {
                ...state,
                survey: {
                    ...state.survey,
                    questions: state.survey.questions.map(x => {
                        if (x.id === action.questionId) {
                            return { ...x, isLoadingOnContext: true };
                        } else {
                            return { ...x };
                        }
                    })
                }
            };
        case receiveSurveyQuestionMutation:
            return {
                ...state,
                survey: {
                    ...state.survey,
                    questions: state.survey.questions.map(x => {
                        if (x.id === action.questionId) {
                            return { ...x, name: action.question.name, isLoadingOnContext: false };
                        } else {
                            return { ...x };
                        }
                    })
                }
            };
            //return (() => {
            //    var newState = { ...state };
            //    for (let el of newState.survey.questions) {
            //        if (el.id === action.questionId) {
            //            el.isLoadingOnContext = true;
            //        }
            //    }
            //    return Object.assign({}, newState);
            //})();
        default:
            return state;
    }
};