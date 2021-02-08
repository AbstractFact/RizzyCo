const refreshMessages = 'REFRESH_MESSAGES';
const initialState = { messages: [], isLoading: false };
const CONTROLLER = 'https://localhost:44348/api/User'

export const actionCreators = {
    sendToQAction: () => async (dispatch) => {
        const url = CONTROLLER + "/SendInvitation";
        const response = await fetch(url);
        dispatch({ res: response.ok });
        //console.log(response.status);
        // fetch(CONTROLLER + "/SendInvitation", {method:"POST", 
        //     headers: {"Content-Type": "application/json"},
        //     body: JSON.stringify({ 
        //         "text": "proba"
        //         })
        //     })
        // .then(response => console.log(response.status))
          }
    // refreshAction: () => async (dispatch) => {
    //     const url = `api/SampleData/Refresh`;
    //     const response = await fetch(url);
    //     const messages = await response.json();

    //     dispatch({ type: refreshMessages, messages });
    // }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === refreshMessages) {
        return {
            ...state,
            messages: action.messages,
            isLoading: false
        };
    }
    return state;
};
